using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDelJocScript : MonoBehaviour
{
    [HideInInspector]
    public int puntuacio = 0;

    private int puntuacioMaxima = 0;

    // Update is called once per frame
    private void Update()
    {
    }

    private void Start()
    {
        puntuacioMaxima = PlayerPrefs.GetInt("PuntuacioMaxima", 0);
    }

    public void Puntua(int quantiat)
    {
        puntuacio += quantiat;
        if (puntuacio > puntuacioMaxima)
        {
            puntuacioMaxima = puntuacio;
            PlayerPrefs.SetInt("PuntuacioMaxima", puntuacioMaxima);
        }
    }

    public void CarregaEscena(int nEscena)
    {
        SceneManager.LoadScene(nEscena, LoadSceneMode.Single);
    }

    public void Surt()

    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR_64
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

//POrtada i dos nivells- entre els dos 3 coses noves que no haguem fet a classe -exemples puntuacio diversa depeenen del item, laberinto, respawn collider invisible amb trigger. final de joc, canvis de mida
// moure camera amb el mouse per exemple // saltar , acceleracio, potencia ,etc... , checkpoints,