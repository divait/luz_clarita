using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {

    public Transform follow_;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - follow_.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = follow_.position + offset;
	}
}
