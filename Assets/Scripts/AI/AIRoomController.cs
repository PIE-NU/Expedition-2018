using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRoomController : MonoBehaviour {

	List<AIFighter> m_aiFighters;



	void Start () {
		CollectFighters ();
	}
	void CollectFighters() {
		m_aiFighters = new List<AIFighter> ();
		foreach (BasicMovement fighterObj in Object.FindObjectsOfType<BasicMovement>()) {
			// Add Fighters that are not the current player
			if (fighterObj.IsCurrentPlayer)
				continue;

			// Collect fighter components of relevance
			AIFighter fighterInfo = new AIFighter ();
			fighterInfo.BasicMove = fighterObj;
			fighterInfo.Fighter = fighterObj.GetComponent<Fighter> ();
			fighterInfo.Attackable = fighterObj.GetComponent<Attackable> ();

			// Catalog fighter
			m_aiFighters.Add (fighterInfo);
		}
	}
	
	void Update () {
		Vector2 target = new Vector2 (0, 0);
		foreach (AIFighter fighter in m_aiFighters) {
			if (Vector2.Distance((Vector2)fighter.BasicMove.transform.position,target) < .1)
				continue;
			fighter.BasicMove.moveToPoint ((Vector3)target);
		}
	}
}
