using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grillos : MonoBehaviour {

    private AudioSource grillos;
    private float timeWait;

	void Start ()
    {
        grillos = GetComponent<AudioSource>();
		
	}
	
	
	void Update () {

        if (grillos.isPlaying)
        {
            return;
        }
        else
        {
            StartCoroutine(TimeOfPlayGrillos());
            
        }
		
	}

    IEnumerator TimeOfPlayGrillos()
    {
        yield return new WaitForSeconds(timeWait);
        timeWait = Random.Range(4, 8);
        grillos.Play();
        
        
    }
}
