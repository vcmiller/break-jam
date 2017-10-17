using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    public Transform[] tracked;
    public Vector2 padding;
    public float minOrthoHeight = 5;

    public float moveSpeedCenter = 1;
    public float moveSpeedGrowth = 1;

    private Vector3 position;

    public Camera camera { get; private set; }

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        position = transform.position;
	}

    public Bounds bounds {
        get {
            Vector2 max = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
            Vector2 min = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

            foreach (Transform t in tracked) {
                min.x = Mathf.Min(t.position.x, min.x);
                min.y = Mathf.Min(t.position.y, min.y);

                max.x = Mathf.Max(t.position.x, max.x);
                max.y = Mathf.Max(t.position.y, max.y);
            }

            min.x -= padding.x;
            min.y -= padding.y;
            max.x += padding.x;
            max.y += padding.y;

            return new Bounds((min + max) / 2, max - min);
        }
    }
	
	// Update is called once per frame
	void Update () {
        var b = bounds;
        float w = b.size.x;
        float h = b.size.y;

        float r = w / h;

        float a = camera.aspect;

        if (r < a) {
            camera.orthographicSize = Mathf.Max(minOrthoHeight, h / 2);
        } else {
            camera.orthographicSize = Mathf.Max(minOrthoHeight, w / a / 2);
        }


        Vector3 v = b.center;
        v.z = position.z;
        position = Vector3.MoveTowards(position, v, moveSpeedCenter + moveSpeedGrowth * Vector3.SqrMagnitude(position - v) * Time.deltaTime);
	}

    private void LateUpdate() {
        transform.position = position;
    }
}
