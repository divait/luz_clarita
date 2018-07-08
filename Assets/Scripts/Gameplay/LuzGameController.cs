using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzGameController : MonoBehaviour {
	public float MAX_DIS = 100.0f;

	public Player[] players;
	public Enemy[] enemies;
	public BossEnemy boss;
    public LuzBehavior luz;
	private bool isPlaying;

	// Use this for initialization
	void Start () {
		isPlaying = true;
	}
	
	// Update is called once per frame
	void Update () {
		// Enemies
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

		// Boss
		float distanceBoss = MAX_DIS;
		int playerIdBoss = -1;
		for(int i=0; i< players.Length;i++) {
			if(players[i] == null) { continue; }

			float d = Vector3.Distance(boss.transform.position, players[i].transform.position);

			if(d < distanceBoss) {
				distanceBoss = d;
				playerIdBoss = i;
			}
		}
		if(playerIdBoss >= 0) {
			boss.goal = players[playerIdBoss];
		}

        // luz
        float distanceLuz = MAX_DIS;
        int playerIdLuz = -1;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) { continue; }

            float d = Vector3.Distance(luz.transform.position, players[i].transform.position);

            if (d < distanceLuz)
            {
                distanceLuz = d;
                playerIdLuz = i;
            }
        }
        if (playerIdLuz >= 0)
        {
            luz.goal = players[playerIdLuz];
        }
    }
}
