using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornarJugarScript : MonoBehaviour
{
    private GameControllerScript controladorDelJocScript;

    // Start is called before the first frame update
    private void Start()
    {
        controladorDelJocScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controladorDelJocScript.CarregaEscena(0);
        }
    }
}