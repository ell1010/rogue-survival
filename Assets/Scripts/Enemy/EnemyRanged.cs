using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyBase {

	// Use this for initialization
	public override void Start () {
		
	}
	

	public override void enemyattack()
	{
		base.enemyattack();
	}
	public override bool checkforplayer()
	{
		return base.checkforplayer();
	}
}
