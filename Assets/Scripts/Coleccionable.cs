using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coleccionable : MonoBehaviour
{
    public static int cantidadColeccionables = 0;
    public Text textoContadorColeccionable;
    ParticleSystem particulasDeColeccionables;

    private void Start()
    {
        // find me trae un gameObject de la jerarquia, asi que debo obtener el atributo
        particulasDeColeccionables = GameObject.Find("ParticulasDeColeccionables").GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            particulasDeColeccionables.transform.position = transform.position;
            particulasDeColeccionables.Play();
            gameObject.SetActive(false);
            cantidadColeccionables++;
            textoContadorColeccionable.text = cantidadColeccionables.ToString();
        }
    }
}
