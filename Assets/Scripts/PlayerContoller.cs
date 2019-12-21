using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerContoller : MonoBehaviour
{

    public static PlayerContoller sharedInstance;
    /*AQUI ESTAN TODOS LOS COMPONENTES */
    //Para poder generar las animaciones
    public Animator animator;
    private Rigidbody2D rg2d;
    private SpriteRenderer spr;

    /*AQUI VA LAS CARACTERISTICAS FISICAS */
    private Vector3 startPosition;

    public float jumpForce = 5f;
    public bool isGrounded;
    public float runningSpeed = 4f;
    private float dirX;
    float currentSpeed;
    private bool movement = true; //Por defecto podemos movernos

    private int healthPoints;

    private int manaPoints;

    //Despertar del juego
    void Awake()
    {
        sharedInstance = this;
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        //Donde empieza la primera vez nuestro personaje
        startPosition = this.transform.position;
    }

    void Start()
    {
        //Iniciamos la animacion
        animator.SetBool("Grounded", true);
        isGrounded = true;
    }

    // Start is called before the first frame update
    public void StartPlayer()
    {
        //Iniciamos la animacion
        animator.SetBool("Grounded", true);
        //Inicializamos parametros cada vez que reiniciamos
        this.transform.position = startPosition;
        this.healthPoints = 100;
        this.manaPoints = 100;

        //Llamamos a la corutina 
        StartCoroutine("tiredPlayer");
    }

    // Update is called once per frame
    void Update()
    {


        //Solo debejos dejar que salte si esta el juego en modo inGame
        if (GameManager.sharedInstance.gameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                //Aqui el usuario acaba de bajar la tecla espacio
                Jump();
            }
            dirX = CrossPlatformInputManager.GetAxis("Horizontal");
            //rg2d.velocity = new Vector2(dirX * runningSpeed,rg2d.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rg2d.velocity.x));
            animator.SetBool("Grounded", isGrounded);
        }
    }

    /* Utilizaremos el fixedUpdate para hacer un llamado de 
        intervalos fijos(cada 0.1 segundos)*/

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.gameState == GameState.inGame)
        {
            Friccion();
            currentSpeed = (runningSpeed - 0.5f) * this.healthPoints / 100.0f;

            if (!movement)
            {
                currentSpeed = 0;
            }
            /* Sitio perfecto para aplicar fuerzas constantes */
            /* if (Input.GetKey(KeyCode.D))
            {
                Run(dirX *currentSpeed);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Run(dirX * currentSpeed);
            }*/
            Run(currentSpeed);
        }
    }
    //Crearemos una funcion para la velocidad
    public void Run(float runSpeed)
    {
        if (-currentSpeed < rg2d.velocity.x && rg2d.velocity.x < currentSpeed)
        {
            rg2d.velocity = new Vector2(runSpeed*dirX, rg2d.velocity.y);
        }

        if (rg2d.velocity.x > 0f)
        {
            rg2d.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rg2d.velocity.x < 0f)
        {
            rg2d.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            //rg2d.velocity = new Vector2(rg2d.velocity.x, 0);
            //F=m*a
            if (this.manaPoints > 15)
            {
                this.manaPoints -= 5;
                //rg2d.AddForce(Vector2.up * jumpForce * 1.2f, ForceMode2D.Impulse);
                rg2d.velocity = new Vector2(rg2d.velocity.x, jumpForce * 1.2f);
            }
            else
            {
                //rg2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                rg2d.velocity = new Vector2(rg2d.velocity.x, jumpForce);
            }
            isGrounded = false;
            
        }
    }

    void Friccion()
    {
        Vector3 fixedVelocity = rg2d.velocity;
        fixedVelocity.x *= 0.75f;

        if (isGrounded)
        {
            rg2d.velocity = fixedVelocity;
        }
    }

    public void Dead(){
        Invoke("KillPlayer", 1.2f);
    }

    public void KillPlayer()
    {
        GameManager.sharedInstance.GameOver();
        /*  Poner animacion de muerte
            this.animator.setBool("isAlive",false) */
        //Obtenemos el maximo score guardado, en caso de ser creado su valor por defecto es 0
        float maxScore = PlayerPrefs.GetFloat("maxScore", 0);

        if (maxScore < this.GetDistance())
        {
            PlayerPrefs.SetFloat("maxScore", this.GetDistance());
        }
        this.rg2d.velocity = new Vector2(0.0f,0.0f);
    }

    public void CollectHealth(int value)
    {
        this.healthPoints += value;

        if (this.healthPoints >= 100)
        {
            this.healthPoints = 100;
        }
    }

    public void CollectMana(int value)
    {
        this.manaPoints += value;

        if (this.manaPoints >= 80)
        {
            this.manaPoints = 80;
        }
    }

    public float GetDistance()
    {
        float distanciaRecorrida = Vector2.Distance(new Vector2(startPosition.x, 0),
                                                    new Vector2(this.transform.position.x, 0));
        // this.transform.position.x - startPosition.x
        return distanciaRecorrida;
    }

    IEnumerator tiredPlayer()
    {
        while (this.healthPoints > 0)
        {
            this.healthPoints-=3;
            yield return new WaitForSeconds(2f);//Espera 1/2 segundo y luego ejecuta bucle
        }
        yield return null;
    }

    public float GetHealthPoints()
    {
        return this.healthPoints;
    }

    public float GetManaPoints()
    {
        return this.manaPoints;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EnemyDamage")
        {
            if (this.transform.position.y < col.transform.position.y + 0.9f)
            {
                this.healthPoints -= 5;
            }
        }
        if (GameManager.sharedInstance.gameState == GameState.inGame && this.healthPoints <= 0)
        {
            Dead();
        }
    }

    public void SaltoSobreEnemigo()
    {
        isGrounded = true;
        Jump();
    }

    public void GolpeDeEnemigo(float enemyPosX)
    {
        isGrounded = true;
        Jump();
        float side = Mathf.Sign(enemyPosX - this.transform.position.x);
        rg2d.AddForce(Vector2.left * side * jumpForce, ForceMode2D.Impulse);
        Color color = new Color(32 / 255f, 132 / 255f, 255 / 255f);
        spr.color = color;
        //spr.color = Color.yellow;
        movement = false;
        Invoke("EnableMovement", 1.2f); //Luego de 0.7 segundos activa el metodo
    }

    public void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }

    public Vector3 getStartPosition(){
        return startPosition;
    }
}
