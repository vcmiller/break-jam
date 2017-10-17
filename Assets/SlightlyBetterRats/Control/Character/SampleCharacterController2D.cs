using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCharacterController2D : PlayerController<CharacterProxy> {
	
	public void Axis_Horizontal(float value) {
        Vector3 right = transform.right;
        right.y = 0;
        right = right.normalized;

        controlled.movement += right * value;
    }

    public void ButtonDown_Jump() {
        controlled.jump = true;
        controlled.jumpHeld = true;
    }

    public void ButtonUp_Jump() {
        controlled.jumpHeld = false;
    }

	public void ButtonDown_Fire(){
		controlled.attack = true;
	}
		
}
