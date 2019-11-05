using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coleccionable : MonoBehaviour
{
    public static int cantidadColeccionables = 0;
    Text textoContadorColeccionable;
    ParticleSystem particulasDeColeccionables;
    AudioSource audioColeccionables;

    private void Start()
    {
        cantidadColeccionables = 0;
        // find me trae un gameObject de la jerarquia, asi que debo obtener el componente propio con getcomponent
        textoContadorColeccionable = GameObject.Find("TextoContadorColeccionable").GetComponent<Text>();
        particulasDeColeccionables = GameObject.Find("ParticulasDeColeccionables").GetComponent<ParticleSystem>();
        // Ahora vamos a obtener el gameobject del padre... recordemos que hay muchas formas de hacer esto
        audioColeccionables = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            particulasDeColeccionables.transform.position = transform.position;
            particulasDeColeccionables.Play();
            audioColeccionables.Play();
            gameObject.SetActive(false);
            cantidadColeccionables++;
            textoContadorColeccionable.text = cantidadColeccionables.ToString();
        }
    }
}
