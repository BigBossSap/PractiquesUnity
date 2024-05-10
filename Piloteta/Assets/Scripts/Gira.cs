using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gira : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 30f) * Time.deltaTime);
    }
}