using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTask : FighterTask 
{
	public int speed = 3;
	public float distanceTilShoot=10f;
	override public void Advance() 
	{
		Vector2 target = Player.transform.position;
		if (Vector2.Distance ((Vector2)Fighter.BasicMove.transform.position, target) < distanceTilShoot)
			print ("Fire!");
		else 
		{
			for (int i = 0; i < speed; i++) {
				Fighter.BasicMove.moveToPoint ((Vector3)target);
			}
		}
	}
}
