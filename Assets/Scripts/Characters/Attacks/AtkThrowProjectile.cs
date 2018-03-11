using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkThrowProjectile : AttackInfo {

	public GameObject ProjectilePrefab;

	protected override void OnAttack()
	{
		var projectile = GameObject.Instantiate(ProjectilePrefab);
		projectile.transform.SetParent(transform);
	}
}
