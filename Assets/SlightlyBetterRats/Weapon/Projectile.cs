using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float launchSpeed;
    public float impactFactor;
    public bool hitsTriggers;
    public bool hitsIfNotFired;
    public float stunTime = 1;
    Vector2 setDirection;
    public Vector3 velocity { get; set; }
    public bool fired { get; private set; }

    public virtual void Fire() {
        Fire(transform.forward);
    }

    public virtual void Fire(Vector2 direction) {
        setDirection = direction;
        velocity = direction.normalized * launchSpeed;
        fired = true;
    }

	protected virtual void OnHitObject(Collider2D col, Vector3 position, Vector3 normal) {
        if (fired || hitsIfNotFired) {
            if (hitsTriggers || !col.isTrigger) {
                velocity = Vector3.zero;

                var player = col.GetComponent<Player>();
                if (player && !player.stunned) {
                    player.Stun(stunTime);
                    player.motor.velocity = Vector2.up * impactFactor + setDirection * impactFactor;
                }

                Destroy(gameObject);
            }
        }
    }
}
