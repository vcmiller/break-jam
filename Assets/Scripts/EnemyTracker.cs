using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTracker : MonoBehaviour {
    public float padding = 50;

    public Camera cam { get; private set; }
    public RectTransform rect { get; private set; }
    public Image image { get; private set; }
    public Transform enemy { get; private set; }

    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        cam = GetComponentInParent<Camera>();
        image = GetComponent<Image>();

        foreach (Player p in FindObjectsOfType<Player>()) {
            if (p.transform != transform.root) {
                enemy = p.transform;
                break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 v = cam.WorldToViewportPoint(enemy.transform.position);

        if (v.x > 0 && v.x < 1 && v.y > 0 && v.y < 1) {
            image.enabled = false;
        } else {
            image.enabled = true;
            v.Scale(cam.pixelRect.size);
            v -= cam.pixelRect.size / 2;

            float a = cam.aspect;
            float s = v.y / v.x;

            if (Mathf.Abs(s) > a) {
                v *= (cam.pixelRect.size.y / 2 - padding) / Mathf.Abs(v.y);
            } else {
                v *= (cam.pixelRect.size.x / 2 - padding) / Mathf.Abs(v.x);
            }


            rect.anchoredPosition = v;
        }

	}
}
