using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandles : MonoBehaviour
{
    public GameObject lighterOB;
    public GameObject flame;
    public GameObject lightText;

    public bool unlit;
    public bool inReach;

    // Reference to the CandleManager
    private CandleManager candleManager;

    // Start is called before the first frame update
    void Start()
    {
        unlit = true;
        flame.SetActive(false);
        lightText.SetActive(false);

        // Find the CandleManager in the scene
        candleManager = FindObjectOfType<CandleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lighterOB.activeInHierarchy && inReach && unlit && Input.GetButtonDown("Interact"))
        {
            flame.SetActive(true);
            lightText.SetActive(false);
            unlit = false;

            // Notify the CandleManager that this candle has been lit
            if (candleManager != null)
            {
                candleManager.CandleLit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && unlit)
        {
            inReach = true;
            lightText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach" && unlit)
        {
            inReach = false;
            lightText.SetActive(false);
        }
    }
}