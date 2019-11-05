using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiadorDeEscena : MonoBehaviour
{
    public void CambiarEscenaA(string nombreDeEscena)
	{
        SceneManager.LoadScene(nombreDeEscena);
	}

    public void ProximaEscena()
	{
        // tomo el indice de la escena actual y le pongo el proximo sumando 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
