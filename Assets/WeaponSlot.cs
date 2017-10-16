using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour {

	public GameObject defaultWeaponPF;
	public AWeapon weapon;
	void Start () {
		GameObject temp;
		temp = Instantiate (defaultWeaponPF, transform.position, transform.rotation);
		PickUp (temp.GetComponent<AWeapon>());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			weapon.activate();
		}
	}

	void PickUp(AWeapon w){
		weapon = w;
		w.transform.parent = transform;
		w.transform.localPosition = Vector3.zero;
	}
}
