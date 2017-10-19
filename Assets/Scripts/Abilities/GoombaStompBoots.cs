using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaStompBoots : AAbility {
    public float stun;
    public float bounce;
    public AudioClip stompSound;

    public override void maybeActivate(CharacterProxy proxy) {
    }

    public override void OnTriggerEnter2D(Collider2D collider) {
        AbilitySlot curSlot = collider.GetComponent<AbilitySlot>();
        if (curSlot && !transform.parent) {
            curSlot.PickUp(this);
            transform.localPosition = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = false;

            var col = GetComponent<BoxCollider2D>();
            col.offset = new Vector2(0, -0.75f);
            col.size = new Vector2(1, 0.5f);
        }

        if (transform.parent && collider.transform != transform.parent) {
            var other = collider.GetComponent<Player>();
            var player = GetComponentInParent<Player>();
            if (other && player && other.motor.velocity.y > player.motor.velocity.y) {
                other.Stun(stun);
                other.motor.velocity.y = -bounce;
                player.motor.velocity.y = bounce;

                AudioSource.PlayClipAtPoint(stompSound, FindObjectOfType<AudioListener>().transform.position, 0.4f);
                CheckDestroy();
            }
        }
    }
}
