using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonsController : MonoBehaviour
{
    public float velocitatDeScroll;
    private float llargadaFons;

    private Vector3 posicioInicial;

    // Start is called before the first frame update
    private void Start()
    {
        posicioInicial = transform.position;
        llargadaFons = transform.localScale.y;
    }

    // Update is called once per frame
    private void Update()
    {
        float desplaçament = Mathf.Repeat(Time.time * velocitatDeScroll, llargadaFons);
        transform.position = posicioInicial + Vector3.back * desplaçament;
    }
}