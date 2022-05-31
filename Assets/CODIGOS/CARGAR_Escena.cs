using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CARGAR_Escena : MonoBehaviour
{
    public Text textoPorcentaje;
    public Text textoCargando;
    float randomMensaje;
    string mensajeCargando;
    public double Porcentaje;
    public string escenaActual;

    void Start()
    {
        randomMensaje = Random.Range(1, 3);
        if (randomMensaje == 1)
        {
            mensajeCargando = "WAIT... MONKEYS AT WORK";
        }
        else if (randomMensaje == 2)
        {
            mensajeCargando = "WAIT... THE CLOWN IS JUMPING";
        }
        else
        {
            mensajeCargando = "WAIT... NO ONE HERE";
        }
        textoCargando.text = mensajeCargando;
        // StartCoroutine(Cargar_Escena_Asincronica());
    }

    IEnumerator Cargar_Escena_Asincronica()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LOGIN");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
            Porcentaje = asyncLoad.progress;
            textoPorcentaje.text = Mathf.RoundToInt((float)Porcentaje).ToString() + "%";
            textoCargando.text = mensajeCargando;

            if (asyncLoad.isDone)
            {
                SceneManager.LoadScene("LOGIN");
            }
        }
    }
}
