using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterTask {
	public Fighter player;
	public AIFighter fighter;

	public void Init(Fighter player, AIFighter fighter) {
		this.player = player;
		this.fighter = fighter;
	}

	virtual public void Advance() {}
}
