using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	Animator anim;
    Player player;
    List<EnemyState> enemysList = new List<EnemyState>();
    // P + P + P + K
    bool isCombo;
    int countP;
    int countK;
    bool punching;
    public float attack = 10.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        isCombo = false;
        punching = false;
        countP = 0;
        countK = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(!player.isAlive()){
            return;
        }

		Vector2 input = new Vector2(
                InputMan.getAxisRaw(InputMan.AXIS.H, player.id), 
                InputMan.getAxisRaw(InputMan.AXIS.V, player.id)
            );
        Vector2 inputDir = input.normalized;

		// Animation 
        if (inputDir != Vector2.zero)
        {
            transform.eulerAngles = Vector3.down * Mathf.Atan2(-inputDir.y, -inputDir.x) * Mathf.Rad2Deg;
            anim.SetBool("moving", true);
        }
        else { anim.SetBool("moving", false); }

		if (isStateName("run") || isStateName("idle2") || isStatePunsh())
        {
            if (InputMan.GetButton(InputMan.BUTTON.B, player.id) )
            {
                countK++;
                if(countP >= 3 && countK == 1) {
                    countP = 0;
                    countK = 0;
                    anim.SetBool("combo", true);
                } else {
                    countP = 0;
                    anim.SetBool("combo", false);
                }
                anim.SetTrigger("kick");
            }

            if (InputMan.GetButton(InputMan.BUTTON.A, player.id))
            {
                Debug.Log("P");
                if(anim.GetFloat("punch_blend") == 0) {
                    anim.SetFloat("punch_blend", 1);
                } else {
                    anim.SetFloat("punch_blend", 0);
                }

                countP++;
                countK = 0;
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

    public bool isStatePunsh() {
        return isStateName("init") || isStateName("punch");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyState e = collision.GetComponent<Enemy>().state;
            if (!enemysList.Contains(e))
            {
                enemysList.Add(e);
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyState e = collision.GetComponent<Enemy>().state;
            if (enemysList.Contains(e))
            {
                enemysList.Remove(e);
            }
        }
    }

    public void hitEnemies ()
    {
        foreach (EnemyState e in enemysList)
        {
            if (e != null)
            {
                e.hurt(attack);
            }
        }
    }
}
