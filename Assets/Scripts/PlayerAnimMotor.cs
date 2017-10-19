using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimMotor : BasicMotor<CharacterProxy> {
    public Animator animator { get; private set; }
    public SpriteRenderer sprite { get; private set; }
    public WallJumpMotor wallMotor { get; private set; }
    public CharacterMotor2D motor { get; private set; }

    public enum State {
        Run, Idle, Wall, Jump
    }

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        wallMotor = GetComponentInParent<WallJumpMotor>();
        motor = GetComponentInParent<CharacterMotor2D>();
    }

    public override void TakeInput() {

        if (motor.grounded) {
            if (control.movement.x != 0) {
                Play(State.Run);
                sprite.flipX = control.movement.x < 0;
            } else {
                Play(State.Idle);
            }
        } else {
            if (wallMotor.onWall) {
                Play(State.Wall);
                sprite.flipX = wallMotor.lastWallDir < 0;
            } else {
                Play(State.Jump);

                if (motor.enableAirControl) {
                    if (control.movement.x != 0)
                    sprite.flipX = control.movement.x < 0;
                } else {
                    sprite.flipX = motor.velocity.x < 0;
                }
            }

        }
        
    }

    private void Play(State state) {
        animator.Play(state.ToString());
    }
}
