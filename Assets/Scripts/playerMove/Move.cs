using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    /*
    public float speed;
    private Vector3 moveDirection = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);

        transform.localEulerAngles = Vector3.up * Mathf.Atan2 (moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
	}*/

    public float speed = 2f;

    bool canWalk;

    Animator anim;
    Collider mCollider;
    CharacterController cha; 

    void Start()
    {
        anim = GetComponent<Animator>();
        mCollider = GetComponent<Collider>();
        cha = GetComponent<CharacterController>();
        mCollider.enabled = true;
        cha.enableOverlapRecovery = true;
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("run") || anim.GetCurrentAnimatorStateInfo(0).IsName("idle2"))
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;

            if (inputDir != Vector2.zero)
            {
                transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
                anim.SetBool("moving", true);
            }
            else { anim.SetBool("moving", false); }

            transform.Translate(new Vector3(inputDir.x, 0, inputDir.y) * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.Q) && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle2") || anim.GetCurrentAnimatorStateInfo(0).IsName("run")) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            anim.SetTrigger("kick");
        }

        if (Input.GetKeyDown(KeyCode.E) && (anim.GetCurrentAnimatorStateInfo(0).IsName("idle2") || anim.GetCurrentAnimatorStateInfo(0).IsName("run")) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            anim.SetTrigger("punch");
        }
    }

}
