using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoots : AAbility {
    public float jumpSpeed;
    public float jumpTime = 0.2f;

    private bool jumping;

    private ExpirationTimer jumpTimer;

    private void Start() {
        jumpTimer = new ExpirationTimer(jumpTime);
    }

    public override void maybeActivate(CharacterProxy proxy) {
        var wall = GetComponentInParent<WallJumpMotor>();
        var motor = GetComponentInParent<CharacterMotor2D>();

        if (proxy.jump && !motor.grounded && !wall.onWall && !motor.jumped && !wall.jumped && !jumping) {
            motor.velocity.y = jumpSpeed;
            jumpTimer.Set();
            jumping = true;
        }
    }

    private void Update() {
        if (jumping) {
            var p = GetComponentInChildren<ParticleSystem>();
            var em = p.emission;

            if (jumpTimer.expired) {
                CheckDestroy();
                em.enabled = false;
                jumping = false;
            } else {
                em.enabled = true;
            }
        }
    }
}
