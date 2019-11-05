using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoDelEnemigo : MonoBehaviour
{
    Rigidbody2D enemigoRb;
    SpriteRenderer enemigoSr;
    Animator enemigoAnimator;
    float tiempoAntesDeCambiar;
    public float delay = 0.5f;
    public float velocidad = 0.3f;
    ParticleSystem particulasDeEnemigos;

    void Start()
    {
        // accedemos a un componente propio de una manera distinta a la convencional
        enemigoRb = GetComponent<Rigidbody2D>();
        enemigoSr = GetComponent<SpriteRenderer>();
        enemigoAnimator = GetComponent<Animator>();
        particulasDeEnemigos = GameObject.Find("ParticulasDeEnemigos").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        enemigoRb.velocity = Vector2.right * velocidad; // Vector2.right es equivalente a Vector2(1, 0) es decir, una versor de componente X
        if (tiempoAntesDeCambiar < Time.time) // si mi tiempo es menor al tiempo que paso desde que la app empezo, entonces...
        {
            velocidad *= (-1); // le cambio la dirección a la velocidad
            tiempoAntesDeCambiar = Time.time + delay; // lo hago mas grande para que cambie la direccion del movimiento en un futuro cercano
        }
        // aprovechando que ahora el signo de "velocidad" me indica la direccion del movimiento... le invertimos la imagen dependiendo el caso
        if (velocidad < 0)
        {
            enemigoSr.flipX = true;
        }
        else if (velocidad > 0)
        {
            enemigoSr.flipX = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if(colision.gameObject.tag == "Player")
        {
            if(transform.position.y + 0.05f < colision.transform.position.y)
            {
                particulasDeEnemigos.transform.position = transform.position;
                particulasDeEnemigos.Play();
                enemigoAnimator.SetBool("estaMuerto", true);
                GetComponentInParent<AudioSource>().Play();
            }
        }
    }

    public void desactivarEnemigo()
    {
        gameObject.SetActive(false);
    }
}
