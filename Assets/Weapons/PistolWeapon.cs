using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : AWeapon {

    public GameObject prefab;
    public override void activate(){
        GameObject projectile = Instantiate(prefab, transform.position, transform.rotation);
        projectile.GetComponent<Projectile>().Fire(GetDirection());
        CheckDestroy();
    }
}
