using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public float life = 100.0f;
	public float attack = 10.0f;
	public float attackTime = 2.0f;
	public Player goal;
	NavMeshAgent agent;
	Animator anim;
	bool isPlayer;
	float time;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		isPlayer = false;
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(goal == null) {
			return;
		}

		if(time > attackTime) {
			time = 0.0f;

			if(!goal.hit(attack)) {
				goal = null;
				isPlayer = false;

				return;
			}
		}

		if(!isPlayer) {
			transform.LookAt(goal.transform);
			agent.destination = goal.transform.position;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player")
        {
			isPlayer = true;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Player")
        {
			isPlayer = false;
		}
	}

	void OnTriggerStay(Collider collision)
    {
		if (collision.gameObject.tag == "Player")
        {
			time += Time.deltaTime;
			anim.SetBool("punching", true);
		}
    }

	void OnTriggerExit(Collider collision) {
		if (collision.gameObject.tag == "Player")
        {
			time += 0.0f;
			anim.SetBool("punching", false);
		}
	}
}
