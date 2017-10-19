using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunWeapon : AWeapon {
	public override void activate(){
		InstantHit ();
		CheckDestroy ();
	}
}
