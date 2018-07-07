using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCharacter : MonoBehaviour {

    public float speed;
	void Start () {
		
	}
	
	
	void Update ()
    {
        MovementCharacter();
		
	}

    void MovementCharacter()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        transform.Translate( moveV * -speed, 0, moveH * speed);
        
    }
}
