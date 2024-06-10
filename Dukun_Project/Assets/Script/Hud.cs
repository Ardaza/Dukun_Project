using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{

    public GameObject flashLightON;
    public GameObject flashLightOFF;
    public GameObject flashLightOB;

    // Start is called before the first frame update
    void Start()
    {
        flashLightON.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (flashLightOB.activeInHierarchy)
        {
            flashLightON.SetActive(true);
            flashLightOFF.SetActive(false);
        }
        else
        {
            flashLightON.SetActive(false);
            flashLightOFF.SetActive(true);
        }
    }
}
