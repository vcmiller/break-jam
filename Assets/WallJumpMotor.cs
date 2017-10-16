using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpMotor : BasicMotor<CharacterProxy> {
    public BoxCollider2D box { get; private set; }
    public CharacterMotor2D mainMotor { get; private set; }

    public float raycastBack = 0.1f;
    public float raycastDist = 0.1f;

    public float fallSpeedOnWall = 2.0f;
    public float wallJumpSpeedX = 10;

    public float wallJumpNoAirControlTime = 0.2f;

    public float stickTime = 0.2f;

    private float inputDir;
    private float lastWallDir;
    private bool onWall;
    private float stuckness = 0;

    protected override void Awake() {
        base.Awake();

        box = GetComponent<BoxCollider2D>();
        mainMotor = GetComponent<CharacterMotor2D>();
    }

    public override void TakeInput() {
        if (control.movement.x != 0) {
            inputDir = Mathf.Sign(control.movement.x);
        } else {
            inputDir = 0;
        }

        if (control.jump && onWall) {
            onWall = false;
            stuckness = 0;
            mainMotor.velocity = new Vector2(-lastWallDir * wallJumpSpeedX, mainMotor.jumpSpeed);

            Invoke("RestoreAC", wallJumpNoAirControlTime);
        }
    }

    void DisableAC() {
        mainMotor.enableAirControl = false;
    }

    void RestoreAC() {
        mainMotor.enableAirControl = true;
    }

    void Detach() {
        stuckness = 0;
        onWall = false;
        RestoreAC();
    }

    void Attach() {
        onWall = true;
        DisableAC();
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 size = box.size - new Vector2(raycastBack, raycastBack);

        if (!mainMotor.grounded) {
            box.enabled = false;
            bool wallLast = lastWallDir != 0 && Physics2D.BoxCast(box.transform.position, size, 0.0f, new Vector2(lastWallDir, 0), raycastBack + raycastDist, mainMotor.queryMask);
            bool wallCur = inputDir != 0 && Physics2D.BoxCast(box.transform.position, size, 0.0f, new Vector2(inputDir, 0), raycastBack + raycastDist, mainMotor.queryMask);
            box.enabled = true;

            if (!onWall) {
                if (wallCur) {
                    stuckness = Mathf.MoveTowards(stuckness, stickTime, Time.deltaTime);
                    lastWallDir = inputDir;

                    if (stuckness == stickTime) {
                        Attach();
                    }
                }
            } else {
                if (!wallCur && wallLast && inputDir != 0) {
                    stuckness = Mathf.MoveTowards(stuckness, 0, Time.deltaTime);

                    if (stuckness == 0) {
                        Detach();
                    }
                } else if (!wallCur && !wallLast) {
                    Detach();
                }
            }
        } else {
            Detach();
        }
        
        if (!onWall) {
            lastWallDir = inputDir = 0;
        }
        
        if (onWall && mainMotor.velocity.y <= -fallSpeedOnWall) {
            mainMotor.velocity.y = -fallSpeedOnWall;
        }
    }
}
