using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : AWeapon {
	public override void activate(){
		InstantHit ();
	}
}
