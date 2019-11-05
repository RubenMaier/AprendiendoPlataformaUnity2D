using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaludJugador : MonoBehaviour
{
    int salud = 3;
    public Image[] corazones;
    bool tieneCooldown = false;
    public CambiadorDeEscena cambioDeEscena;


    private void OnCollisionEnter2D(Collision2D colision)
    {
        if(colision.gameObject.CompareTag("Enemigo"))
        {
            if(GetComponent<MovimientoJugador>().estaEnElSuelo)
            {
                RestarSalud();
            }
        }
    }

    void RestarSalud()
    {
        if(!tieneCooldown)
        {
            if(salud > 0)
            {
                salud--;
                tieneCooldown = true;
                StartCoroutine(Cooldown());
            }
        }
        if(salud <= 0)
        {
            cambioDeEscena.CambiarEscenaA("EscenaDePerder");
        }

        VaciarCorazones();
    }

    void VaciarCorazones()
    {
        for(int i = 0; i < corazones.Length; i++)
        {
            if(salud -1 < i) corazones[i].gameObject.SetActive(false);
        } 
    }

    // para reiniciar el cooldown vamos a usar una coorutina
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f); // medio segundo
        tieneCooldown = false;
        StopCoroutine(Cooldown());
    }
}
