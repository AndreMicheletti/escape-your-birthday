using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateColor : ClickAction
{
    public string color;
    public Camera playerCamera;
    public Light dirLight;
    public AudioSource audioSource;
    private void Start() {
        tag = "Clickable";
    }

    public override bool canUse()
    {
        return true;
    }

    public override void OnUsed()
    {
        if (!canUse()) return;
        int colorMask;
        Color camColor;
        switch (color) {
            case "Blue":
                colorMask = (int) Math.Pow(2, 6);
                camColor = Color.blue;
                break;
            case "Red":
                colorMask = (int) Math.Pow(2, 7);
                camColor = Color.red;
                break;
            case "Yellow":
                colorMask = (int) Math.Pow(2, 8);
                camColor = Color.yellow;
                break;
            case "Green":
                colorMask = (int) Math.Pow(2, 9);
                camColor = Color.green;
                break;
            default:
                colorMask = 0;
                camColor = Color.white;
                break;
        }
        if (dirLight.color != camColor) {
            Debug.Log("Changing Color to " + color);
            playerCamera.cullingMask = 63 + colorMask;
            dirLight.color = camColor;
            if (audioSource != null) {
                audioSource.Play();
            }
            dirLight.intensity = 1f;
        }
    }
}
