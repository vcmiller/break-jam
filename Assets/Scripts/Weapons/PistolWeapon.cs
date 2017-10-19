using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : AWeapon {

    public GameObject prefab;
    public override void activate(){
		Vector3 dir = GetDirection ();
		GameObject projectile = Instantiate(prefab, transform.position + new Vector3(0f,.4f,0f) + 0.3f*dir, transform.rotation);
		projectile.GetComponent<SpriteRenderer> ().flipX = dir.x<0;
        projectile.GetComponent<Projectile>().Fire(dir);
        CheckDestroy();
    }
}
