using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LuzBehavior : MonoBehaviour {

    public Player goal;
    NavMeshAgent agent;
    Animator anim;
    float initSpeed;
    string goalsName;

    public bool algo;


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        goalsName = "";
        initSpeed = agent.speed;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(goal.transform.name);
        if (goal == null)
        {
            return;
        }

        transform.LookAt(goal.transform);
        agent.destination = goal.transform.position;

        if (/*anim.GetCurrentAnimatorStateInfo(0).IsName("weak") || anim.GetCurrentAnimatorStateInfo(0).IsName("punch") || anim.GetCurrentAnimatorStateInfo(0).IsName("dead")*/algo == true)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = initSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
}
