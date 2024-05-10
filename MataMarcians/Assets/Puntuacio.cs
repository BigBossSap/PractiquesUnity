using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntuacio : MonoBehaviour
{
    // Start is called before the first frame update
    private GameControllerScript controladorDelJocScript;

    public TextMeshProUGUI txtPuntuacio;

    // Start is called before the first frame update
    private void Start()
    {
        controladorDelJocScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        txtPuntuacio.text = "Guanyador!\r\n\r\nEsc per tornar a jugar \r\n Puntuació final: " + controladorDelJocScript.puntuacio;
    }
}