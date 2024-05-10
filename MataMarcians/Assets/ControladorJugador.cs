using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Limits
{
    public float xMin, xMax, zMin, zMax;
}

public class ControladorJugador : MonoBehaviour
{
    public float velocitat;
    public float balanceig;
    public Limits limits;
    private Rigidbody rb;
    public GameObject projectil;
    public GameObject explosioJugador;

    // public GameObject powerUpMulti;
    public GameObject[] armes = new GameObject[3];

    private GameControllerScript controladorDelJocScript;
    public float fireDelta = 0.5F;
    private int multishot = 1;
    public int vides = 3;
    public TextMeshProUGUI txtVides;
    public TextMeshProUGUI txtInvencible;
    public TextMeshProUGUI txtVelocitat;
    private bool invulnerable = false;
    private float invulnerableFins = 0.0f;
    public float tempsInvulnerable = 5.0f;
    public GameObject shield;

    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    private void Start()
    {
        vides = 3;
        velocitat = 10;
        rb = GetComponent<Rigidbody>();
        txtVides.text = "Vides: " + vides;
        txtVelocitat.text = "Velocitat: " + velocitat;
        shield.SetActive(false);
        controladorDelJocScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    private void Update()
    {
        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            Instantiate(projectil, armes[0].transform.position, transform.rotation);
            if (multishot > 1)
            {
                Instantiate(projectil, armes[1].transform.position, transform.rotation);
                if (multishot > 2)
                {
                    Instantiate(projectil, armes[2].transform.position, transform.rotation);
                }
            }

            nextFire = myTime + fireDelta;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float movimentHoritzontal = Input.GetAxis("Horizontal");
        float movimentVertical = Input.GetAxis("Vertical");

        Vector3 moviment =
            new Vector3(movimentHoritzontal, 0.0f, movimentVertical);

        rb.velocity = moviment * velocitat;

        rb.position = new Vector3(
             Math.Clamp(rb.position.x, limits.xMin, limits.xMax),
            rb.position.y,
             Math.Clamp(rb.position.z, limits.zMin, limits.zMax)
            );

        rb.rotation = Quaternion.Euler(0F, 0F, -moviment.x * balanceig);
        if (invulnerable)
        {
            float remainingTime = invulnerableFins - Time.time;
            txtInvencible.text = "Invulnerable: " + remainingTime.ToString("F1") + "s";
        }
        else
        {
            txtInvencible.text = "";
        }
        if (Time.time > invulnerableFins)
        {
            invulnerable = false;
            if (shield.activeSelf)
            {
                shield.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemic"))
        {
            if (invulnerable)
            {
                return;
            }
            vides -= 1;
            txtVides.text = "Vides: " + vides;
            Destroy(other.gameObject);
            GameObject clon = Instantiate(explosioJugador, this.transform.position, this.transform.rotation);
            Destroy(clon, 2000);
            if (vides == 0)
            {
                txtVides.text = "Rip";
                Destroy(this.gameObject);
                clon = Instantiate(explosioJugador, this.transform.position, this.transform.rotation);
                Destroy(clon, 2000);
                controladorDelJocScript.CarregaEscena(1);
            }
        }

        if (other.tag.Equals("Multi"))
        {
            if (multishot < 4)
            {
                multishot += 1;
            }
            else
                fireDelta -= 0.1f;
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("Velocitat"))
        {
            velocitat += 5;
            txtVelocitat.text = "Velocitat: " + velocitat;
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("Vida"))
        {
            if (vides < 3)
            {
                vides += 1;
                txtVides.text = "Vides: " + vides;
            }
            Destroy(other.gameObject);
        }
        if (other.tag.Equals("Escut"))
        {
            shield.SetActive(true);
            invulnerable = true;
            invulnerableFins = Time.time + tempsInvulnerable;
            Destroy(other.gameObject);
        }
    }

    public void RestarVida()
    {
        vides -= 1;
    }

    //IDEAS- Armas diferents power ups, puntuacio diferents, vida per asteroide, vida per jugador, muniicio, jefe final(pintaxungo)
    // temps de powerup. shop, dificultads, musica, efectes de so, pantalla de game over, pantalla de victoria, pantalla de inicio, pantalla de pausa
}