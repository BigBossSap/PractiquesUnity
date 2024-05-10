using System.Collections;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public int puntuacio = 0;
    public GameObject LimitEsquerra;
    public GameObject LimitDret;
    public GameObject[] Asteroides;
    public GameObject[] PowerUps;
    public float pausaEntreAsterorides;
    public int nAsteroridesPerOnada;
    public TextMeshProUGUI txtPuntuacio;
    public int powerUpSpawnChance;
    private bool powerUpShown = false;
    public AudioSource audioSource;
    private int nRondes;
    public TextMeshProUGUI txtRondes;
    public GameObject jefe;
    private bool jefeSpawned = false;
    private GameObject jefeInstance;

    public void Puntua(int punts)
    {
        puntuacio += punts;
        ActualitzaMarcador();
    }

    private void Start()
    {
        puntuacio = 0;
        ActualitzaMarcador();
        nRondes = 0;
        StartCoroutine(Partida());
    }

    private void ActualitzaMarcador()
    {
        txtPuntuacio.text = puntuacio + " punts";
    }

    private IEnumerator GeneraOnadaAsteroides()
    {
        nRondes++;
        txtRondes.text = "Ronda " + nRondes + "/6";
        if (nRondes <= 1)
        {
            powerUpShown = false;
            int powerup = Random.Range(1, nAsteroridesPerOnada);
            for (int nAsteroides = 0; nAsteroides < nAsteroridesPerOnada; nAsteroides++)
            {
                GameObject asteroideNou = Asteroides[Random.Range(0, Asteroides.Length)];
                GameObject powerUpNou = PowerUps[Random.Range(0, PowerUps.Length)];
                Vector3 posicioAsteroides = new Vector3(
                    Random.Range(LimitEsquerra.transform.position.x,
                    LimitDret.transform.position.x),
                    LimitEsquerra.transform.position.y,
                    LimitEsquerra.transform.position.z);
                Instantiate(asteroideNou, posicioAsteroides, Quaternion.identity);
                if (nAsteroides == powerup && !powerUpShown)
                {
                    Vector3 posicioPowerUp = new Vector3(
                    Random.Range(LimitEsquerra.transform.position.x,
                    LimitDret.transform.position.x - 1),
                    LimitEsquerra.transform.position.y,
                    LimitEsquerra.transform.position.z);
                    Instantiate(powerUpNou, posicioPowerUp, Quaternion.identity);
                    powerUpShown = true;
                }
                yield return new WaitForSeconds(pausaEntreAsterorides);
            }
        }
        else
        {
            Vector3 posicioAsteroides = new Vector3(
   (LimitEsquerra.transform.position.x + LimitDret.transform.position.x) / 2,
   LimitEsquerra.transform.position.y,
   LimitEsquerra.transform.position.z);
            jefeInstance = Instantiate(jefe, posicioAsteroides, Quaternion.identity);
            jefeInstance.transform.rotation = Quaternion.identity;
            jefeSpawned = true;
            yield return new WaitForSeconds(30);
        }
    }

    private IEnumerator Partida()
    {
        while (true)
        {
            yield return GeneraOnadaAsteroides();
            pausaEntreAsterorides *= 0.9f;
            nAsteroridesPerOnada = (int)(nAsteroridesPerOnada * 1.1);
            yield return new WaitForSeconds(3);
        }
    }

    // Start is called before the first frame update

    private void Update()
    {
        if (jefeInstance == null && jefeSpawned)
        {
            CarregaEscena(2);
            jefeSpawned = false;
            audioSource.Stop();
        }
    }

    public void CarregaEscena(int nEscena)
    {
        SceneManager.LoadScene(nEscena, LoadSceneMode.Single);
        audioSource.Stop();
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