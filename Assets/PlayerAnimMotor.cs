using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimMotor : BasicMotor<CharacterProxy> {
    public Animator animator { get; private set; }
    public SpriteRenderer sprite { get; private set; }

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public override void TakeInput() {
        animator.SetBool("Running", control.movement.x != 0);

        if (control.movement.x != 0) {
            sprite.flipX = control.movement.x < 0;
        }
    }
}
