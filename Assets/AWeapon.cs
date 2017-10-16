using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour {

	public float range; // how far the range of attack is
	public float impactFactor; //how much force will be applied to the enemy player 
	public int numUse; //how many times can use before it dissapears

	void Start () {
		//Position randomly in the world
		//associate with an image
	}
	
	// Update is called once per frame
	void Update () {
		//maybe make the thing turn or light up
	}

	public abstract void activate (); //Fire stufff


	void OnTriggerEnter2D(Collider2D collider){
		WeaponSlot curSlot = collider.GetComponent<WeaponSlot>();
		curSlot.PickUp (this);
		GetComponent<Collider2D> ().enabled = false;

	}
}
