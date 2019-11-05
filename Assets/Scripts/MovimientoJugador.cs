using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public Rigidbody2D jugadorRb;
    public float velocidadCaminata = .5f;
    public float velocidadSalto = 150;

    public bool estaEnElSuelo = true;

    public Animator AnimatorDelJugador;

    public AudioClip audioCaminata;
    public AudioClip audioSalto;
    AudioSource sonidosJugador;

    bool caminataActivada = false;

    private void Start()
    {
        sonidosJugador = GetComponent<AudioSource>();
    }

    void Update()
    {
        jugadorRb.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadCaminata, jugadorRb.velocity.y);
        // GeyKey | mientras la tecla este precionada...
        // GetKeyDown | cuando se toca una tecla...
        // GetKeyUp | cuando se suelta una tecla precionada ...
        // Ojo para que sea universal de teclado y joistick hay que usar lo mismo pero con GetButton___

        // si preciono la tecla Jump... configurada en el Input Managment
        if (Input.GetButtonDown("Jump") && jugadorRb.velocity.y >= 0 && estaEnElSuelo) // la segunda condicion es para corregir un bug de doblesalto cuando el jugador se cae de una plataforma
        {
            caminataActivada = false;
            sonidosJugador.loop = false;
            sonidosJugador.Stop();
            sonidosJugador.PlayOneShot(audioSalto, 1);
            AnimatorDelJugador.SetTrigger("salto"); // como es un trigger solo con el set lo ejecuto
            jugadorRb.AddForce(Vector2.up * velocidadSalto); // le creo un vector fuerza hacia arriba... Vector2.up es un versor en realidad creo...
            estaEnElSuelo = false; // ahora no esta en el piso porque esta saltando
        }

        // ¿hacia donde vamos? ¿estamos caminando o no?
        if (Input.GetAxis("Horizontal") == 0) // -1 vamos a la izquierda | 1 vamos a la derecha | 0 estamos quietos en el medio
        {
            if (caminataActivada)
            {
                caminataActivada = false;
                sonidosJugador.loop = false;
                sonidosJugador.Stop();
            }
            // como accedemos a los parametros del animator? debemos acceder al componente
            AnimatorDelJugador.SetBool("estaCaminando", false); // le seteamos la variable en false porque no esta caminando
        }
        else if (Input.GetAxis("Horizontal") < 0) // estoy caminando a izquierda, asi que volteo la animacion
        {
            if (!caminataActivada && estaEnElSuelo)
            {
                caminataActivada = true;
                sonidosJugador.PlayOneShot(audioCaminata, 0.7f);
                sonidosJugador.loop = true;
            }
            AnimatorDelJugador.SetBool("estaCaminando", true);
            // recordemos que con GetComponent puedo obtener los componentes pertenecientes al propio objeto
            GetComponent<SpriteRenderer>().flipX = true; // Le invierto en X la dirección porque se mueve hacia la izquierda y por default el sprit esta hacia la derecha
        }
        else // por defecto va a la derecha
        {
            if (!caminataActivada && estaEnElSuelo)
            {
                caminataActivada = true;
                sonidosJugador.PlayOneShot(audioCaminata, 0.7f);
                sonidosJugador.loop = true;
            }
            AnimatorDelJugador.SetBool("estaCaminando", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        // como se que toco el piso y no un enemigo?
        if(colision.gameObject.CompareTag("Piso"))
        {
            estaEnElSuelo = true; // como esta en el piso nuevamente, le ponemos el parametro
        }
    }
}
