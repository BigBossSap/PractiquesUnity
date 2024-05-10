using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornar : MonoBehaviour
{
    private ControladorDelJocScript controladorDelJocScript;

    // Start is called before the first frame update
    private void Start()
    {
        controladorDelJocScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControladorDelJocScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controladorDelJocScript.puntuacio = 0;
            controladorDelJocScript.CarregaEscena(0);
        }
    }
}