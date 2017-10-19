using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] above;
    public GameObject[] same;
    public GameObject[] below;

    public float sameRange = 3;
    public float spawnLifetime = 5;
    public float chancePerSecond = 0.5f;

    private ExpirationTimer killSpawned;
    private GameObject spawned;

    private Player[] players;

	// Use this for initialization
	void Start () {
        players = FindObjectsOfType<Player>();
        killSpawned = new ExpirationTimer(spawnLifetime);
	}

    private void Update() {
        if (spawned.transform.parent) {
            spawned = null;
        }

        if (spawned && killSpawned.expired) {
            Destroy(spawned);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {
            if (spawned == null && Random.value < chancePerSecond * Time.deltaTime) {
                SpawnRandom(below);
            }
        }
    }

    private void SpawnRandom(GameObject[] options) {
        spawned = Instantiate(options[Random.Range(0, options.Length)], transform.position, transform.rotation);

        killSpawned.Set();
    }
}
