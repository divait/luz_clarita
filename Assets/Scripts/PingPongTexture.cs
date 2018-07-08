using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongTexture : MonoBehaviour {

    public float speed = 1.0f;
    public Color init;
    public Color end;

    public bool repeatable = false;
    private float startTime;

	void Start ()
    {
       
        startTime = Time.time;
	}


    void Update()
    {
        if (!repeatable)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.SetColor("_TintColor", Color.Lerp(init, end, t));
        }
        else
        {
            float t = (Mathf.Sin(Time.time - startTime) * speed);
            GetComponent<Renderer>().material.SetColor("_TintColor", Color.Lerp(init, end, t));

        }
	}
}
