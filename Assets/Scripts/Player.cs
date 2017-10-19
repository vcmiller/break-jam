using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Transform lastCheckpoint;
    private CharacterMotor2D motor;
    public float stunTime = 0.5f;
    public bool isBlue;

	// Use this for initialization
	void Start () {
        motor = GetComponent<CharacterMotor2D>();
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
}
