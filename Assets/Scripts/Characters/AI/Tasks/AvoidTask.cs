
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidTask : FighterTask 
{
	public int speed = 3;
	override public void Advance() 
	{
		Vector2 target = WalkAway(Player.transform.position, Fighter.Fighter.transform.position);
		for (int i = 0; i < speed; i++) {
			Fighter.BasicMove.moveToPoint((Vector3)target);
		}
	}

	// Calculates the vector leading away from the player.
	Vector2 WalkAway(Vector2 p, Vector2 e) 
	{
		Vector2 away = e-p;
		return 2 * away;
	} 
}
