using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDelJugador : MonoBehaviour
{
    private ControladorDelJocScript controladorDelJocScript;
    private SceneManager Scenemanager;
    public float velocidad = 100.0f;
    public float forcaDeSalt = 5f;
    public TextMeshProUGUI txtPuntuacio;
    public TextMeshProUGUI txtVelocitat;
    public float jumpAmount = 10;
    private Rigidbody rb;
    private bool canJump;
    public float velocidadRotacion = 2.0f;
    public float maxSpeed = 10f;
    public Transform mainCameraTransform;
    private int marcadorVelocitat;
    public AudioSource audioSource;
    public Vector3 cameraOffset = new Vector3(0f, 1.5f, -3f);
    private Vector3 initialPosition;
    private Vector3 initialRotation;

    private void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            // Calcular nova posició de la càmera
            Vector3 rotatedOffset = mainCameraTransform.rotation * cameraOffset;
            mainCameraTransform.position = transform.position + rotatedOffset;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controladorDelJocScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDelJocScript>();
        initialPosition = transform.position;
        initialRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        marcadorVelocitat = (int)rb.velocity.magnitude;
        txtVelocitat.text = "Velocitat: " + marcadorVelocitat + " km/h";

        float movVertical = Input.GetAxis("Vertical");
        float movHortzontal = Input.GetAxis("Horizontal");

        movVertical *= Time.deltaTime;
        movHortzontal *= Time.deltaTime;

        Vector3 movementDirection = CalcularDireccioMoviment(movHortzontal, movVertical);

        rb.AddForce(movementDirection * velocidad);

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }

        // Rotacio camera amb el ratoli
        float mouseX = Input.GetAxis("Mouse X") * velocidadRotacion;
        mainCameraTransform.RotateAround(transform.position, Vector3.up, mouseX);

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Limit
        float velocityMagnitude = rb.velocity.magnitude;
        if (velocityMagnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            txtPuntuacio.text = "Puntuació: " + controladorDelJocScript.puntuacio + "/180 Punts";

            if (controladorDelJocScript != null && controladorDelJocScript.puntuacio >= 180)
            {
                controladorDelJocScript.CarregaEscena(2);
                controladorDelJocScript.puntuacio = 0;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            txtPuntuacio.text = "Puntuació: " + controladorDelJocScript.puntuacio + "/150 Punts";
        }
    }

    private Vector3 CalcularDireccioMoviment(float horizontalInput, float verticalInput)
    {
        if (mainCameraTransform != null)
        {
            Vector3 cameraForward = mainCameraTransform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 cameraRight = mainCameraTransform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            return (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;
        }
        else
        {
            Debug.LogWarning("Main camera transform not found.");
            return Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int puntos = 0;

        switch (other.gameObject.tag)
        {
            case "Ficha10":
                puntos = 10;
                break;

            case "Figura5":
                puntos = 5;
                break;

            case "Figura3":
                puntos = 3;
                break;

            case "Figura1":
                puntos = 1;
                break;

            default:
                break;
        }

        if (puntos > 0)
        {
            Destroy(other.gameObject);
            controladorDelJocScript.Puntua(puntos);
            audioSource.Play();
        }
    }

    private void Saltar()
    {
        rb.AddForce(Vector3.up * forcaDeSalt, ForceMode.Impulse);
        canJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Limits"))
        {
            transform.position = initialPosition;
            controladorDelJocScript.puntuacio = 0;
            controladorDelJocScript.CarregaEscena(2);
        }

        if (other.gameObject.CompareTag("Meta"))
        {
            if (controladorDelJocScript != null && controladorDelJocScript.puntuacio >= 150)
            {
                controladorDelJocScript.CarregaEscena(3);
            }
            else
            {
                controladorDelJocScript.puntuacio = 0;
                controladorDelJocScript.CarregaEscena(2);
            }
        }
    }
}