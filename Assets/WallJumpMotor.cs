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
    public Vector2 wallJumpSpeed;
    public Vector2 wallJumpAccel;
    public float wallJumpAccelTime;

    public float wallJumpNoAirControlTime = 0.2f;

    public float stickTime = 0.2f;

    public float inputDir { get; private set; }
    public float stuckness { get; private set; }
    public float lastWallDir { get; private set; }
    public bool onWall { get; private set; }
    public bool jumped { get; private set; }
    public ExpirationTimer wallJumpAccelTimer { get; private set; }

    protected override void Awake() {
        base.Awake();

        box = GetComponent<BoxCollider2D>();
        mainMotor = GetComponent<CharacterMotor2D>();
        wallJumpAccelTimer = new ExpirationTimer(wallJumpAccelTime);
    }

    public override void TakeInput() {
        jumped = false;

        if (control.movement.x != 0) {
            inputDir = Mathf.Sign(control.movement.x);
        } else {
            inputDir = 0;
        }

        if (control.jump && onWall) {
            onWall = false;
            stuckness = 0;
            mainMotor.velocity = new Vector2(-lastWallDir * wallJumpSpeed.x, wallJumpSpeed.y);
            jumped = true;
            Invoke("RestoreAC", wallJumpNoAirControlTime);
            wallJumpAccelTimer.Set();
        }

        if (!wallJumpAccelTimer.expired) {
            if (control.jumpHeld) {
                mainMotor.velocity += new Vector2(-lastWallDir * wallJumpAccel.x, wallJumpAccel.y) * Time.deltaTime;
            } else {
                wallJumpAccelTimer.Clear();
            }
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
            bool collider = box.enabled;
            bool startIn = Physics2D.queriesStartInColliders;
            bool triggers = Physics2D.queriesHitTriggers;

            box.enabled = false;
            Physics2D.queriesHitTriggers = false;
            Physics2D.queriesStartInColliders = false;

            bool wallLast = lastWallDir != 0 && Physics2D.BoxCast(box.transform.position, size, 0.0f, new Vector2(lastWallDir, 0), raycastBack + raycastDist, mainMotor.queryMask);
            bool wallCur = inputDir != 0 && Physics2D.BoxCast(box.transform.position, size, 0.0f, new Vector2(inputDir, 0), raycastBack + raycastDist, mainMotor.queryMask);

            box.enabled = collider;
            Physics2D.queriesHitTriggers = triggers;
            Physics2D.queriesStartInColliders = startIn;

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
            inputDir = 0;
        }
        
        if (onWall && mainMotor.velocity.y <= -fallSpeedOnWall) {
            mainMotor.velocity.y = -fallSpeedOnWall;
        }
    }
}
