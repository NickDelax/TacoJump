using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlesSimples : MonoBehaviour
{
    [HideInInspector] public bool mirandoDerecha = true;
    [HideInInspector] public bool salto = false;

    public float fuerzaMovimiento = 365f;
    public float maxVelocidad = 5f;
    public float fuerzaSalto = 1350f;
    public Transform controlSuelo;
    public string escena = "";
    public GameObject panelWin, panelGameOver;

    private bool enSuelo = false, pisando = true;
    private Animator anim;
    private Rigidbody2D rb2d;

    void Awake(){
        anim = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        escena = SceneManager.GetActiveScene().name;
        panelGameOver.SetActive(false);
        panelWin.SetActive(false);
    }

    void Start()
    {
        puntaje = 0;
        ActualizarMarcador();
    }

    void Update()
    {
        enSuelo = Physics2D.Linecast(transform.position, controlSuelo.position, 1 << LayerMask.NameToLayer("Suelo"));
        if (Input.GetButtonDown("Jump") && pisando)
        {
            salto = true;
            pisando = false;
        }
    }
    void Flip()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Velocidad", Mathf.Abs(h));
        if (h * rb2d.velocity.x < maxVelocidad)
            rb2d.AddForce(Vector2.right * h * fuerzaMovimiento);
        if (Mathf.Abs(rb2d.velocity.x) > maxVelocidad)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxVelocidad, rb2d.velocity.y);
        if (h > 0 && !mirandoDerecha)
            Flip();
        else if (h < 0 && mirandoDerecha)
            Flip();
        if (!enSuelo) { anim.SetTrigger("Salto"); }
        if (enSuelo) { anim.ResetTrigger("Salto"); }
        if (salto)
        {
            rb2d.AddForce(new Vector2(0f, fuerzaSalto));
            salto = false;
        }
    }
    public Text textoPuntaje;
    private int puntaje;



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo"))
        {
            pisando = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Moneda"))
        {
            other.gameObject.SetActive(false);
            puntaje = puntaje + 10;
            ActualizarMarcador();
            Win();

        }
        if (other.gameObject.CompareTag("Espinas"))
        {
            GameOver();
        }
    }

    void ActualizarMarcador()
    {
        textoPuntaje.text = "Puntos: " + puntaje.ToString();
    }


    void Win()
    {
       
        if (escena == "Level001")
        {
            if (puntaje > 75)
            {
                panelWin.SetActive(true);
                Invoke("Nivel2", 2);
            }
        }
        if (escena == "Level002")
        {
            if (puntaje > 155)
            {
                panelWin.SetActive(true);
                Invoke("Nivel3", 2);
            }
        }
        if (escena == "Level003")
        {
            if (puntaje > 115) 
            {
                panelWin.SetActive(true);
                Invoke("Menu", 2);
            }
        }
    }

    void GameOver()
    {
        if (escena == "Level001")
        {
            panelGameOver.SetActive(true);
            Invoke("Nivel1", 2);
        }
        if (escena == "Level002")
        {
            panelGameOver.SetActive(true);
            Invoke("Nivel2", 2);

        }
        if (escena == "Level003")
        {
            panelGameOver.SetActive(true);
            Invoke("Nivel3",2);
        }
    }

    void Nivel1()
    {
        SceneManager.LoadScene("Level001");
    }
    void Nivel2()
    {
        SceneManager.LoadScene("Level002");
    }
    void Nivel3()
    {
        SceneManager.LoadScene("Level003");
    }
    void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
   
}
