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

		if(!state.isAlive() && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead")) {
			StartCoroutine(death());
			return;
		}

		transform.LookAt(goal.transform);
		agent.destination = goal.transform.position;

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("dead") || anim.GetCurrentAnimatorStateInfo(0).IsName("punch")) {
			agent.speed = 0;
		} else {
			agent.speed = initSpeed;
		}

		if(playersList.Count > 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("walk")) {
			anim.SetBool("punching", true);
		} else if(playersList.Count == 0) {
			anim.SetBool("punching", false);
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
		List<Player> kills = new List<Player>();
		foreach (Player p in playersList) {
			if(p != null) {
				if(!p.hurt(state.attack)) {
					kills.Add(p);
				}
			}
		}
		foreach (Player p in kills) {
			playersList.Remove(p);
			if(p.id == goal.id) {
				goal = null;
			}
		}
	}

	IEnumerator death() {
		anim.SetTrigger("dead");
		foreach(CapsuleCollider c in GetComponents<CapsuleCollider>()) {
			c.enabled = false;
		}
		goal = null;
		enabled = false;

		yield return new WaitForSeconds(2.0f);

		Destroy(gameObject);
	}
}
