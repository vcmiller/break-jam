﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : BasicMotor<CharacterProxy> {

	public GameObject defaultWeaponPF;
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
        w.transform.localScale = new Vector3(0.04f, 0.04f);
		

    }

	public void GetInitWeapon(){
		GameObject temp;
		temp = Instantiate (defaultWeaponPF, transform.position, transform.rotation);
		PickUp (temp.GetComponent<AWeapon>());
	}
}
