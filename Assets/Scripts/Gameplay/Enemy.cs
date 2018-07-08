using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public Player goal;
	public EnemyState state;

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

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("dead") || anim.GetCurrentAnimatorStateInfo(0).IsName("punch")) {
			agent.speed = 0;
		} else {
			agent.speed = initSpeed;
		}

		Debug.Log("WERWE: " + playersList.Count );
		if(playersList.Count > 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("walk")) {
			Debug.Log("PUNCH");
			anim.SetTrigger("punching");
			
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

	public void hit() {
		foreach (Player p in playersList) {
				if(p != null) {
					p.hurt(state.attack);
				}
			}
	}
}
