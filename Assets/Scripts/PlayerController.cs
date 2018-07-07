using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
       /* public float speed;
    public GameObject body;
    
        void Start()
        {

        }


        void Update()
        {
            MovementCharacter();
        Debug.Log("h: " + Input.GetAxis("Horizontal") + "v: " + Input.GetAxis("Vertical"));

        }

        void MovementCharacter()
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");

        transform.Translate((moveV * -speed) * Time.deltaTime, 0, (moveH * speed) * Time.deltaTime)  ;
        if (Input.GetAxis("Horizontal") > 0)
        {
            body.transform.rotation = new Quaternion(0, 180, 0, 0);
        } else if (Input.GetAxis("Horizontal") < 0)
        {
            body.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        else if (Input.GetAxis("Vertical") > 0)
        {
            body.transform.rotation = new Quaternion(0, 90, 0, 0);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            body.transform.rotation = new Quaternion(0, 270, 0, 0);
        }

    }*/

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
}
