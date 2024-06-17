using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class advancedFlashlight : MonoBehaviour
{
    public Light light;
    public TMP_Text text;

    public TMP_Text batteryText;

    public float lifetime = 100;

    public float batteries = 0;

    public AudioSource flashON;
    public AudioSource flashOFF;

    private bool on;
    private bool off;

    public GameObject flashlight;


    void Start()
    {
        light = GetComponent<Light>();

        off = true;
        on = false;
        light.enabled = false;
        flashlight.SetActive(false);
    }



    void Update()
    {
        text.text = lifetime.ToString("0") + "%";
        batteryText.text = batteries.ToString();

        if (Input.GetButtonDown("flashlight") && off)
        {
            flashlight.SetActive(true);
            flashON.Play();
            light.enabled = true;
            on = true;
            off = false;
        }

        else if (Input.GetButtonDown("flashlight") && on)
        {
            flashlight.SetActive(false);
            flashOFF.Play();
            light.enabled = false;
            on = false;
            off = true;
        }

        if (on)
        {
            lifetime -= 0.5f * Time.deltaTime;
        }

        if (lifetime <= 0)
        {
            light.enabled = false;
            on = false;
            off = true;
            lifetime = 0;
        }

        if (lifetime >= 100)
        {
            lifetime = 100;
        }

        if (Input.GetButtonDown("reload") && batteries >= 1)
        {
            batteries -= 0.5f;
            lifetime += 50;
        }

        if (Input.GetButtonDown("reload") && batteries == 0)
        {
            return;
        }

        if (batteries <= 0)
        {
            batteries = 0;
        }

    }
}
