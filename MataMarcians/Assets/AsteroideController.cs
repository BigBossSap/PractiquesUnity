using UnityEngine;

public class AsteroideController : MonoBehaviour
{
    public int puntuacio = 10;
    public int resistencia = 10;

    public GameObject explosio;
    private GameObject clon;
    private GameControllerScript gameControllerScript;

    // Start is called before the first frame update
    private int resistenciaActual = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.tag.Equals("Frontera")) && other.gameObject.layer != LayerMask.NameToLayer("PowerUps"))
        {
            if (other.gameObject.tag == "Projectil")
            {
                resistenciaActual--;
                if (resistenciaActual <= 0)
                {
                    gameControllerScript.Puntua(puntuacio);
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                    clon = Instantiate(explosio, this.transform.position, this.transform.rotation);
                }
                else
                {
                    Destroy(other.gameObject);
                    clon = Instantiate(explosio, this.transform.position, this.transform.rotation);
                }
            }
        }
    }

    private void Start()
    {
        gameControllerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        resistenciaActual = resistencia;
    }
}