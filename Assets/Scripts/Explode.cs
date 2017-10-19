using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
    public GameObject explosionPrefab;

    public void OnHit() {
        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
    }
}
