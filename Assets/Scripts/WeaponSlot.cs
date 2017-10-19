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
		if(control.attack){
			weapon.activate();		
		}
	}

	public void PickUp(AWeapon w){
		//Destroy (weapon.gameObject); 
		weapon = w;
		w.transform.parent = transform;
    }

	public void GetInitWeapon(){
		GameObject temp;
		temp = Instantiate (defaultWeaponPF, transform.position, transform.rotation);
		PickUp (temp.GetComponent<AWeapon>());
	}
}
