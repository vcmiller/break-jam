using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour {

	public float range; // how far the range of attack is
	public float impactFactor; //how much force will be applied to the enemy player 
	public int numUse; //how many times can use before it dissapears
	public LayerMask lm;
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
		Destroy(curSlot.weapon.gameObject);
		curSlot.PickUp (this);
		GetComponent<Collider2D> ().enabled = false;
	}

	public Vector2 GetDirection(){
		var sprite = transform.root.GetComponent<SpriteRenderer> ();
		if (sprite.flipX) {
			return Vector2.left;
		} else {
			return Vector2.right;
		}
	}

	public void InstantHit(){
		Vector2 vector = GetDirection(); 
		RaycastHit2D[] hitArr = Physics2D.RaycastAll (transform.position, vector, range, lm.value); //To be changed
		foreach (RaycastHit2D hit in hitArr) {
			if (hit.transform != transform.root) {
				var motor = hit.collider.GetComponent<CharacterMotor2D> ();
				motor.velocity = Vector2.up * impactFactor + vector*impactFactor;
			} 
		}
	}

	public void CheckDestroy(){
		numUse--;
		if (numUse == 0) {
			WeaponSlot wp = transform.parent.GetComponent<WeaponSlot> ();
			wp.GetInitWeapon();
			Destroy (gameObject);
		}
	}
}
