using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinicialock : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}