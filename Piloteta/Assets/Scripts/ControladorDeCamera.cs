using UnityEngine;

public class ControladorDeCamera : MonoBehaviour
{
    public GameObject jugador;
    public Vector3 offset = new Vector3(0f, 1.5f, -3f); // Offset
    public float alturaDeSalt = 1.0f;
    public float velocitatRotacio = 2.0f; // Velocidat de rotació

    private float rotacioY = 0f;

    private void LateUpdate()
    {
        // Rotación horizontal de la cámara con el mouse
        float mouseX = Input.GetAxis("Mouse X") * velocitatRotacio;
        rotacioY += mouseX;

        // Rotate the camera around the player horizontally
        Quaternion targetRotation = Quaternion.Euler(0f, rotacioY, 0f);
        Vector3 cameraPosition = jugador.transform.position + targetRotation * offset;

        transform.rotation = targetRotation;
        transform.position = cameraPosition;
    }
}