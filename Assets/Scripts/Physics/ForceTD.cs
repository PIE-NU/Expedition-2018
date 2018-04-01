using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTD {
	public Vector2 Force;
	public float Duration;

	public ForceTD(Vector2 force, float dur)
	{
		Force = force;
		Duration = dur;
	}

	public ForceTD()
	{
		Force = new Vector2(0,0);
		Duration = 0;
	}
}
