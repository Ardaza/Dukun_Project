using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampuKedapKedip : MonoBehaviour
{

    public Light light08;

    public AudioSource lightsound;

    public float minTime;
    public float maxTime;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        LightFlickering();
    }

    void LightFlickering()
    {
       if (timer > 0)
       {
            timer -= Time.deltaTime;
       }

       if (timer <= 0)
       {
            light08.enabled = !light08.enabled;
            timer = Random.Range(minTime, maxTime);
            lightsound.Play();
       }
    }
}
