using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTask : FighterTask 
{
	public int speed = 3;
	override public void Advance() 
	{
		Vector2 target = Player.transform.position;
		if (Vector2.Distance((Vector2)Fighter.BasicMove.transform.position, target) < .1f)
			return;
		for (int i = 0; i < speed; i++)
		{
			Fighter.BasicMove.moveToPoint((Vector3)target);
		}
	}
}
