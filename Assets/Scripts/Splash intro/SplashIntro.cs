using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashIntro : MonoBehaviour {

    public float time = 1.5f;
    private AudioSource buttonClick;

	void Start () {

       buttonClick = GetComponent<AudioSource>();
	}
	
	
	void Update () {

        if (Input.anyKeyDown)
        {
            buttonClick.Play();
            Invoke("LoadVideoScene", time);
        }
    }
    void LoadVideoScene()
    {
        SceneManager.LoadScene(1);
    }


    }
