using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pausa : MonoBehaviour
{
    public bool isPaused = false;
    public KeyCode pauseKey = KeyCode.Escape;
    public TextMeshProUGUI pausa;

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausa.text = "PAUSA";
        }
        else
        {
            if (!isPaused)
            {
                Time.timeScale = 1f;
                pausa.text = "";
            }
        }
    }
}