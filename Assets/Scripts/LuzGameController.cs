using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		if(luz == null || !havePlayers()) {
			StartCoroutine(restarLevel());
		}

		// Enemies
		foreach(Enemy e in enemies) {
			if(e == null) { continue; }
			int playerId = getNearPlayer(e.transform, false);
            Debug.Log(playerId);
			if(playerId >= 0) {
                Debug.Log("Add: " + e);
				e.goal = players[playerId];
			}
		}

		// Boss
		int playerIdBoss = getNearPlayer(boss.transform, false);
		if(playerIdBoss >= 0) {
			boss.goal = players[playerIdBoss];
		}

		// Luz
		if(luz != null) {
			int playerIdLuz = getNearPlayer(luz.transform, true);
			if(playerIdLuz >= 0) {
				luz.goal = players[playerIdLuz];
			}
		}
	}

	int getNearPlayer(Transform element, bool isLuz) {
		float distance = MAX_DIS;
		int playerId = -1;
		for(int i=0; i< players.Length;i++) {
			if(players[i] == null || (isLuz && i == players.Length-1)) { continue; }

			float d = Vector3.Distance(element.position, players[i].transform.position);

            if (d < distance) {
				distance = d;
				playerId = i;
			}
		}

		return playerId;
	}

	IEnumerator restarLevel () {
		yield return new WaitForSeconds(5.0f);

		SceneManager.LoadScene("map");
	}

	bool havePlayers() {
		bool hPlayers = false;
		foreach(Player p in players) {
			if(p != null && players[players.Length -1] != p) {
				hPlayers = true;
				break;
			}
		}

		return hPlayers;
	}
}
