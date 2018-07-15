using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LuzBehavior : MonoBehaviour {

    public Player goal;
    public AudioClip[] audios;
    NavMeshAgent agent;
    Animator anim;
    AudioSource source;
    float initSpeed;
    string goalsName;
    Player player;


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        player = GetComponentsInChildren<Player>()[0];
        goalsName = "";
        initSpeed = agent.speed;
    }
	
	// Update is called once per frame
	void Update () {
        GetComponentsInChildren<Transform>()[0].position = transform.position;

        if (goal == null) 
        {
            return;
        }

        if(!player.isAlive() && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead")) {
			StartCoroutine(death());
			return;
		}

        transform.LookAt(goal.transform);
        agent.destination = goal.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(goal == null) { return ; }

        if (other.transform.name  == goal.transform.parent.name)
        {
            agent.isStopped = true;
            agent.speed = 0;
            anim.SetBool("stop", true);
            goalsName = other.transform.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == goalsName)
        {
            agent.isStopped = false;
            agent.speed = initSpeed;
            anim.SetBool("stop", false);
            goalsName = "";
            agent.destination = goal.transform.position;
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

    public void makeSound(int index) {
        if (index >=0 && index < audios.Length) {
            source.clip = audios[index];
            
            source.Play();
        }
    }
}
