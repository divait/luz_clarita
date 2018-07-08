using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoIntro : MonoBehaviour {

    private VideoPlayer vp;

	void Start()
    {

        vp = GetComponent<VideoPlayer>();
    }


    void Update()
    {

        Invoke("Loadvideo", 56);

    }

    void Loadvideo()
    {
        if (vp.isPlaying == false)
        {
            SceneManager.LoadScene(2);

        }
        else
        {

            return;
        }
    }
}