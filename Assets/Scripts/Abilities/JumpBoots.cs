using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoots : AAbility {
    public float jumpSpeed;

    public override void maybeActivate(CharacterProxy proxy) {
        var wall = GetComponentInParent<WallJumpMotor>();
        var motor = GetComponentInParent<CharacterMotor2D>();

        if (proxy.jump && !motor.grounded && !wall.onWall && !motor.jumped && !wall.jumped) {
            motor.velocity.y = jumpSpeed;
            CheckDestroy();
        }
    }
}
