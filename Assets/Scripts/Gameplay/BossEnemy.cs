using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour {

	public float life = 100.0f;
	public float attack = 10.0f;
	public Player goal;
	NavMeshAgent agent;
	Animator anim;
	float initSpeed;

	List<Player> playersList = new List<Player>();

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();

		initSpeed = agent.speed;
	}
	
	// Update is called once per frame
	void Update () {
		if(goal == null) {
			return;
		}

		transform.LookAt(goal.transform);
		agent.destination = goal.transform.position;

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("weak") || anim.GetCurrentAnimatorStateInfo(0).IsName("punch")) {
			agent.speed = 0;
		} else {
			agent.speed = initSpeed;
		}

		if(playersList.Count > 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("walking")) {
			anim.SetTrigger("punch");
			
		}
	}

	public void hit() {
		foreach (Player p in playersList) {
				if(p != null) {
					p.hurt(attack);
				}
			}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player" && goal != null && goal.isAlive())
        {
			// TODO
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "Player")
        {
			// TODO
		}
	}
	
	void OnTriggerEnter(Collider collision)
    {
		if (collision.gameObject.tag == "Player")
        {
			Player p = collision.GetComponent<MovePhysic>().player;
			if(!playersList.Contains(p))
			{
				playersList.Add(p);
			}
		}
	}

	void OnTriggerStay(Collider collision)
    {
		if (collision.gameObject.tag == "Player")
        {
			// TODO
		}
    }

	void OnTriggerExit(Collider collision) {
		if (collision.gameObject.tag == "Player")
        {
			Player p = collision.GetComponent<MovePhysic>().player;
			if(playersList.Contains(p))
			{
				playersList.Remove(p);
			}
		}
	}
}
