using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	Animator anim;
    Player player;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2(
                InputMan.getAxisRaw(InputMan.AXIS.H, player.id), 
                InputMan.getAxisRaw(InputMan.AXIS.V, player.id)
            );
        Vector2 inputDir = input.normalized;

		// Animation 
		if (isStateName("run") || isStateName("idle2"))
        {
            if (inputDir != Vector2.zero)
            {
                transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
                anim.SetBool("moving", true);
            }
            else { anim.SetBool("moving", false); }

            if (InputMan.GetButton(InputMan.BUTTON.B, player.id))
            {
                anim.SetTrigger("kick");
            }

            if (InputMan.GetButton(InputMan.BUTTON.A, player.id))
            {
                anim.SetTrigger("punch");
            }
        }
	}

	void FixedUpdate()
    {
		Vector2 input = new Vector2(
                InputMan.getAxisRaw(InputMan.AXIS.H, player.id), 
                InputMan.getAxisRaw(InputMan.AXIS.V, player.id)
            );
        Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero)
		{
			transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
		}
	}

    public bool isStateName( string name) {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
