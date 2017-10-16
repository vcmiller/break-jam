using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunWeapon : AWeapon {
	public LayerMask lm;
	public override void activate(){
		RaycastHit2D[] hitArr = Physics2D.RaycastAll (transform.position, Vector2.right, range, lm.value); //To be changed
		foreach (RaycastHit2D hit in hitArr) {
			if (hit.transform != transform.root) {
				var motor = hit.collider.GetComponent<CharacterMotor2D> ();
				motor.velocity = Vector2.up * impactFactor + Vector2.right*impactFactor;
			} 
		}
		numUse--;
		if (numUse == 0) {
			//destroy
		}
	}
}
