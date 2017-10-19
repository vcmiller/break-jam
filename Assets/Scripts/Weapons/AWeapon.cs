using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour {

	public float range; // how far the range of attack is
	public float impactFactor; //how much force will be applied to the enemy player 
	public int numUse; //how many times can use before it dissapears
    public int pose;
	public LayerMask lm;
    public float stun = 0.5f;

	void Start () {
		//Position randomly in the world
		//associate with an image
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent && transform.GetComponent<SpriteRenderer>())
        {
            SpriteRenderer player =  transform.parent.GetComponent<SpriteRenderer>();
            transform.localPosition = Vector2.zero;// new Vector2(0, .3f) + 0.8f * GetDirection();
            transform.GetComponent<SpriteRenderer>().flipX = player.flipX;
        }
	}

	public abstract void activate (); //Fire stufff


	void OnTriggerEnter2D(Collider2D collider){
		WeaponSlot curSlot = collider.GetComponent<WeaponSlot>();
        if (curSlot.weapon) {
            Destroy(curSlot.weapon.gameObject);
        }
        collider.GetComponent<Animator>().SetInteger("weaponPose", pose);
        curSlot.PickUp (this);
		GetComponent<Collider2D> ().enabled = false;
	}

	public Vector2 GetDirection(){
		var sprite = transform.root.GetComponent<SpriteRenderer> ();
		if (sprite.flipX) {
			return Vector2.left;
		} else {
			return Vector2.right;
		}
	}

	public void InstantHit(){
		Vector2 vector = GetDirection(); 
		RaycastHit2D[] hitArr = Physics2D.RaycastAll (transform.position, vector, range, lm.value); //To be changed
		foreach (RaycastHit2D hit in hitArr) {
			if (hit.transform != transform.root) {
                var player = hit.collider.GetComponent<Player>();
                if (player && !player.stunned) {
                    player.motor.velocity = Vector2.up * impactFactor + vector * impactFactor;
                    player.Stun(stun);
                }
			}
		}
	}

	public void CheckDestroy(){
		numUse--;
		if (numUse == 0) {
			WeaponSlot wp = transform.parent.GetComponent<WeaponSlot> ();
			wp.GetInitWeapon();
			Destroy (gameObject);
		}
	}
}
