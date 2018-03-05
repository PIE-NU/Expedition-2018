using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendPointTask : FighterTask {

	public Vector2 center;
	public float radius;

	override public void Advance() {
		Vector2 target = DefendPoint(center, radius);
		if (Vector2.Distance((Vector2)fighter.BasicMove.transform.position, target) < .1f)
			return;
		fighter.BasicMove.moveToPoint ((Vector3)target);
	}

	Vector2 DefendPoint(Vector2 center, float radius) {
		Vector2 x = player.transform.position;
		Vector2 d = x-center;
		d.Normalize ();
		return center + (d * radius);
	} 
}
