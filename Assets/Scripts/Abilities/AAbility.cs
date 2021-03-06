﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAbility : MonoBehaviour {
    public int numUse; //how many times can use before it dissapears
    public Color bootColor;
    
    public abstract void maybeActivate(CharacterProxy proxy);
    
    public virtual void OnTriggerEnter2D(Collider2D collider) {
        AbilitySlot curSlot = collider.GetComponent<AbilitySlot>();
        if (curSlot && !transform.parent) {
            curSlot.PickUp(this);
            transform.localPosition = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void CheckDestroy() {
        numUse--;
        if (numUse == 0) {
            Destroy(gameObject);
        }
    }

    public Vector2 GetDirection() {
        var sprite = transform.root.GetComponent<SpriteRenderer>();
        if (sprite.flipX) {
            return Vector2.left;
        } else {
            return Vector2.right;
        }
    }
}
