using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LuzBehavior : MonoBehaviour {

    public Player goal;
    NavMeshAgent agent;
    Animator anim;
    float initSpeed;

    public bool algo;


    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        initSpeed = agent.speed;
    }
	
	// Update is called once per frame
	void Update () {
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
}
