using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRoomController : MonoBehaviour {

	List<AIFighter> m_aiFighters;
	List<FighterTask> m_tasks;
	Fighter player;


	void Start () {
		CollectFighters ();
		CreateTasks ();
	}

	void CollectFighters() {
		m_aiFighters = new List<AIFighter> ();
		foreach (BasicMovement fighterObj in Object.FindObjectsOfType<BasicMovement>()) {
			// Add Fighters that are not the current player
			if (fighterObj.IsCurrentPlayer) {
				player=fighterObj.GetComponent<Fighter> ();
				continue;
			}
				
				

			// Collect fighter components of relevance
			AIFighter fighterInfo = new AIFighter ();
			fighterInfo.BasicMove = fighterObj;
			fighterInfo.Fighter = fighterObj.GetComponent<Fighter> ();
			fighterInfo.Attackable = fighterObj.GetComponent<Attackable> ();

			// Catalog fighter
			m_aiFighters.Add (fighterInfo);
		}
	}

	void CreateTasks() {
		m_tasks = new List<FighterTask> ();
		int count = 0;
		foreach (AIFighter fighter in m_aiFighters) {
			DefendPointTask task = new DefendPointTask ();
			task.Init (player, fighter);
			task.center = new Vector2 (1, 1);
			task.radius = (count % 2 == 0) ? 1f : 2f;
			m_tasks.Add (task);
			count++;
		}
	}

	void Update () {
		foreach (FighterTask task in m_tasks) {
			task.Advance ();
		}
	}
}
