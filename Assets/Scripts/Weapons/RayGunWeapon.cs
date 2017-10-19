using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayGunWeapon : AWeapon {
	public GameObject preFab;
	public override void activate(){
		InstantHit ();
		GameObject ray = Instantiate (preFab, transform.position + shootOffset, transform.rotation);
		ray.transform.localScale = GetDirection () + new Vector2(0, 1);
		Destroy (ray, .1f);
		CheckDestroy ();
	}
}
