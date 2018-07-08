﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzGameController : MonoBehaviour {
	public float MAX_DIS = 100.0f;

	public Player[] players;
	public Enemy[] enemies;
	private bool isPlaying;

	// Use this for initialization
	void Start () {
		isPlaying = true;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Enemy e in enemies) {
			float distance = MAX_DIS;
			int playerId = -1;
			for(int i=0; i< players.Length;i++) {
				if(players[i] == null) { continue; }

				float d = Vector3.Distance(e.transform.position, players[i].transform.position);

				if(d < distance) {
					distance = d;
					playerId = i;
				}
			}
			if(playerId >= 0) {
				e.goal = players[playerId];
			}
		}
	}
}
