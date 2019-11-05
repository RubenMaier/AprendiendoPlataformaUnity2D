using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaComportamiento : MonoBehaviour
{
	public CambiadorDeEscena cambioDeEscena;

    void OnTriggerEnter2D(Collider2D colli)
	{
        if(colli.CompareTag("Player"))
		{
			cambioDeEscena.ProximaEscena();
		}
	}
}
