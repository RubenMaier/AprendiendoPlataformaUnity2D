using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public Rigidbody2D jugadorRb;
    public float velocidadCaminata = .5f;
    public float velocidadSalto = 150;

    bool estaEnElSuelo = true;

    public Animator AnimatorDelJugador;

    void Update()
    {
        jugadorRb.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadCaminata, jugadorRb.velocity.y);
        // GeyKey | mientras la tecla este precionada...
        // GetKeyDown | cuando se toca una tecla...
        // GetKeyUp | cuando se suelta una tecla precionada ...
        // Ojo para que sea universal de teclado y joistick hay que usar lo mismo pero con GetButton___

        // ¿hacia donde vamos? ¿estamos caminando o no?
        if (Input.GetAxis("Horizontal") == 0) // -1 vamos a la izquierda | 1 vamos a la derecha | 0 estamos quietos en el medio
        {
            // como accedemos a los parametros del animator? debemos acceder al componente
            AnimatorDelJugador.SetBool("estaCaminando", false); // le seteamos la variable en false porque no esta caminando
        }
        else if (Input.GetAxis("Horizontal") < 0) // estoy caminando a izquierda, asi que volteo la animacion
        {
            AnimatorDelJugador.SetBool("estaCaminando", true);
            // recordemos que con GetComponent puedo obtener los componentes pertenecientes al propio objeto
            GetComponent<SpriteRenderer>().flipX = true; // Le invierto en X la dirección porque se mueve hacia la izquierda y por default el sprit esta hacia la derecha
        }
        else // por defecto va a la derecha
        {
            AnimatorDelJugador.SetBool("estaCaminando", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (estaEnElSuelo)
        {
            // si preciono la tecla Jump... configurada en el Input Managment
            if (Input.GetButtonDown("Jump") && jugadorRb.velocity.y >= 0) // la segunda condicion es para corregir un bug de doblesalto cuando el jugador se cae de una plataforma
            {
                AnimatorDelJugador.SetTrigger("salto"); // como es un trigger solo con el set lo ejecuto
                jugadorRb.AddForce(Vector2.up * velocidadSalto); // le creo un vector fuerza hacia arriba... Vector2.up es un versor en realidad creo...
                estaEnElSuelo = false; // ahora no esta en el piso porque esta saltando
            }
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
