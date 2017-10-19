using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : BasicMotor<CharacterProxy> {

	public GameObject defaultWeaponPF;

    [HideInInspector]
	public AWeapon weapon;
	//Controller, Proxy, Motor
	void Start () {
		GetInitWeapon ();
	}


	public override void TakeInput (){
		if (weapon && control.attack) {
			weapon.activate();
            GetComponent<Player>().Freeze(weapon.selfFreeze);
		}
	}

	public void PickUp(AWeapon w){
		//Destroy (weapon.gameObject); 
		weapon = w;
		w.transform.parent = transform;
        w.transform.localScale = Vector3.one;
    }

	public void GetInitWeapon(){
		GameObject temp;
		temp = Instantiate (defaultWeaponPF, transform.position, transform.rotation);
		PickUp (temp.GetComponent<AWeapon>());
	}
}
