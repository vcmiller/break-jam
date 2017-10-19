using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDownWeapon : AWeapon {
	public GameObject projectile;

	public override void activate() {
		GameObject newProj = Instantiate(projectile, transform.position, transform.rotation);
		newProj.GetComponent<Projectile>().Fire(Vector2.down);
		CheckDestroy();
	}
}
