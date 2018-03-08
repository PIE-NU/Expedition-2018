using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RaycastOrigins
{
	public Vector2 topLeft, topRight, bottomLeft, bottomRight;
}

public struct CollisionInfo
{
	public bool above, below, left, right;

	public void Reset() {
		above = below = left = right = false;
	}
}
