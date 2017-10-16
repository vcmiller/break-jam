using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : AWeapon {
	public LayerMask lm;
	public override void activate(){
		RaycastHit2D[] hitArr = Physics2D.RaycastAll (transform.position, Vector2.right, 2, lm.value); //To be changed
		foreach (RaycastHit2D hit in hitArr) {
			if (hit.transform != transform.root) {
				var motor = hit.collider.GetComponent<CharacterMotor2D> ();
				motor.velocity = Vector2.up * 20 + Vector2.right*20;
			} 
		}
	}
}
