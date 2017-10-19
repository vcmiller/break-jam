using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Transform lastCheckpoint;
    public float stunTime = 0.5f;
    public bool isBlue;

    public bool stunned {
        get {
            return !stunTimer.expired;
        }
    }

    public ExpirationTimer stunTimer { get; private set; }
    public CharacterMotor2D motor { get; private set; }
    public CharacterProxy proxy { get; private set; }
    public SampleCharacterController2D control { get; private set; }

	// Use this for initialization
	void Start () {
        motor = GetComponent<CharacterMotor2D>();
        stunTimer = new ExpirationTimer(1);
        proxy = GetComponent<CharacterProxy>();
        control = GetComponent<SampleCharacterController2D>();
	}

    private void Update() {
        control.enabled = stunTimer.expired;
        motor.enableInput = stunTimer.expired;
    }

    // Update is called once per frame

    void EnableMovement() {
        motor.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Checkpoint")) {
            lastCheckpoint = collision.transform;
        } else if (collision.CompareTag("Spikes") && lastCheckpoint) {
            transform.position = lastCheckpoint.position;
            motor.velocity = Vector3.zero;
            motor.enabled = false;
            Invoke("EnableMovement", stunTime);
        }
    }

    public void Stun(float duration) {
        if (!stunned) {
            stunTimer.expiration = duration;
            stunTimer.Set();
        }
    }
}
