using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public Rigidbody2D jugadorRb;
    public float velocidadCaminata = .5f;
    public float velocidadSalto = 150;
    
    void Start()
    {
        
    }

    void Update()
    {
        jugadorRb.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidadCaminata, jugadorRb.velocity.y);
        // GeyKey | mientras la tecla este precionada...
        // GetKeyDown | cuando se toca una tecla...
        // GetKeyUp | cuando se suelta una tecla precionada ...
        // Ojo para que sea universal de teclado y joistick hay que usar lo mismo pero con GetButton___
        if (Input.GetButtonDown("Jump")) // si preciono la tecla Jump... configurada en el Input Managment
        {
            jugadorRb.AddForce(Vector2.up * velocidadSalto); // le creo un vector fuerza hacia arriba... Vector2.up es un versor en realidad creo...
        }
    }
}
