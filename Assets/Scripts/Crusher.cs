using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour {
    public float timeA = 1;
    public float timeB = 1;
    public float speedAToB = 10;
    public float speedBToA = 10;

    public float dist = 2;

    public int state = 0;
    public float timerOffset = 0;

    private float timer;
    private Vector3 aPos;
    private Vector3 bPos;

	// Use this for initialization
	void Start () {
		if (state == 0) {
            timer = timeA - timerOffset;
        } else if (state == 2) {
            timer = timeB - timerOffset;
        }

        aPos = transform.position;
        bPos = transform.position + transform.right * dist;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0 || state == 2) {
            timer -= Time.deltaTime;

            if (timer <= 0) {
                state++;
            }
        } else if (state == 1) {
            transform.position = Vector3.MoveTowards(transform.position, bPos, Time.deltaTime * speedAToB);

            if (transform.position == bPos) {
                state = 2;
                timer = timeB;
            }
        } else if (state == 3) {
            transform.position = Vector3.MoveTowards(transform.position, aPos, Time.deltaTime * speedBToA);

            if (transform.position == aPos) {
                state = 0;
                timer = timeA;
            }
        }
	}
}
