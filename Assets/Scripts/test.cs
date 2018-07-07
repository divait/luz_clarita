using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    private bool grounded = false;
    Rigidbody rigid;



    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;
        rigid.useGravity = false;
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (grounded)
        {

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;

            if (inputDir != Vector2.zero)
            {
                transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            }

            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigid.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rigid.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        // We apply gravity manually for more tuning control
        rigid.AddForce(new Vector3(0, -gravity * rigid.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }
}
