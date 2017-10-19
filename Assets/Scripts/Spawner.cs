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
    private SpriteRenderer sprite;

    private Player[] players;

	// Use this for initialization
	void Start () {
        players = FindObjectsOfType<Player>();
        killSpawned = new ExpirationTimer(spawnLifetime);
        sprite = GetComponent<SpriteRenderer>();
	}

    private void Update() {
        sprite.enabled = !spawned;

        if (spawned && spawned.transform.parent) {
            spawned = null;
        }

        if (spawned && killSpawned.expired) {
            Destroy(spawned);
        }

        if (spawned) {
            spawned.transform.localScale = new Vector3(Mathf.Sin(Time.time * 4), 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Player>()) {

            if (spawned == null && Random.value < chancePerSecond * Time.deltaTime) {
                Transform otherPlayer;

                if (collision.transform == players[0].transform) {
                    otherPlayer = players[1].transform;
                } else {
                    otherPlayer = players[0].transform;
                }

                if (otherPlayer.position.y > collision.transform.position.y + sameRange) {
                    SpawnRandom(below);
                } else if (otherPlayer.position.y < collision.transform.position.y - sameRange) {
                    SpawnRandom(above);
                } else {
                    SpawnRandom(same);
                }

                
            }
        }
    }

    private void SpawnRandom(GameObject[] options) {
        spawned = Instantiate(options[Random.Range(0, options.Length)], transform.position, transform.rotation);

        killSpawned.Set();
    }
}
