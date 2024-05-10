using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moure : MonoBehaviour
{
    public float velocitat;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * velocitat;
    }
}