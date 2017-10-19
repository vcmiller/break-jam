using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBoots : AAbility {
    public float dashSpeed;
    public float dashTime;

    private bool dashing;
    private Vector3 dashVel;

    public float stun;
    public float push;

    private ExpirationTimer dashTimer;

    private void Start() {
        dashTimer = new ExpirationTimer(dashTime);
    }

    private void OnDestroy() {
        if (dashing) {
            var ctrl = GetComponentInParent<SampleCharacterController2D>();
            var motor = GetComponentInParent<CharacterMotor2D>();

            ctrl.enabled = true;
            motor.enableInput = true;
            motor.velocity = dashVel * 0.25f;
        }
    }

    public override void maybeActivate(CharacterProxy proxy) {
        var wall = GetComponentInParent<WallJumpMotor>();
        var motor = GetComponentInParent<CharacterMotor2D>();

        if (proxy.jump && !motor.grounded && !wall.onWall && !motor.jumped && !wall.jumped && !dashing) {
            dashing = true;
            dashTimer.Set();
            dashVel = GetDirection() * dashSpeed;
        }
    }

    private void Update() {
        var ctrl = GetComponentInParent<SampleCharacterController2D>();
        var motor = GetComponentInParent<CharacterMotor2D>();
        var p = GetComponentInChildren<ParticleSystem>();
        var em = p.emission;

        if (dashing) {
            if (!dashTimer.expired) {
                ctrl.enabled = false;
                motor.enableInput = false;
                motor.velocity = dashVel;
                em.enabled = true;
                p.transform.forward = -GetDirection();
            } else {
                ctrl.enabled = true;
                motor.enableInput = true;
                dashing = false;
                motor.velocity = dashVel * 0.25f;
                em.enabled = false;
                CheckDestroy();
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collider) {
        AbilitySlot curSlot = collider.GetComponent<AbilitySlot>();
        if (curSlot && !transform.parent) {
            curSlot.PickUp(this);
            transform.localPosition = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;
        }

        if (transform.parent && collider.transform != transform.parent && dashing) {
            var other = collider.GetComponent<Player>();
            var player = GetComponentInParent<Player>();
            if (other && player) {
                other.Stun(stun);
                other.motor.velocity = dashVel * push;
            }
        }
    }


}
