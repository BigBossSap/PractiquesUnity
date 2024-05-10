using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacio : MonoBehaviour
{
    // Start is called before the first frame update

    public float velocitatRotacio;

    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * velocitatRotacio;
    }
}