using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float life = 100.0f;
    public int id = 1;

	bool alive;

    // Use this for initialization
	void Start () {
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (life < 0) {
			alive = false;
		}
	}

	public bool hit(float damage) {
		life -= damage;
		return isAlive();
	}

	public bool isAlive() {
		return alive;
	}
}