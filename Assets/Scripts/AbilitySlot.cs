using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlot : BasicMotor<CharacterProxy> {

    [HideInInspector]
    public AAbility ability;

    public SpriteRenderer boots;

    private void Update() {
        if (ability) {
            boots.enabled = true;
            boots.color = ability.bootColor * (2 + Mathf.Sin(Time.time));
        } else {
            boots.enabled = false;
        }
    }

    public void PickUp(AAbility a) {
        //Destroy (weapon.gameObject); 
        if (ability) {
            Destroy(ability);
        }

        ability = a;
        a.transform.parent = transform;
    }

    public override void TakeInput() {
        if (ability) {
            ability.maybeActivate(control);
        }
    }
}
