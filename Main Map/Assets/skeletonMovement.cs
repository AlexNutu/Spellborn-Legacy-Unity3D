using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using UnityEngine.AI;

public class skeletonMovement : MonoBehaviour {

	private Transform player = null;
    Animator anim;
    private Vector3 skeletonDefaultPosition;
    NavMeshAgent nav;
	public int health;
	private bool blocked;
	public GameObject damager;

    void Start () {
        anim = GetComponent<Animator>();
        skeletonDefaultPosition = this.transform.position;
        nav = GetComponent<NavMeshAgent>();
		blocked = false;
		health = 100;
    }

	void OnTriggerEnter(Collider other) {
		if (blocked)
			return;
		if (player != null)
			return;
		if (other.tag == "Player") player = other.transform;
	}

	void OnTriggerStay(Collider other){
		if (blocked)
			return;
		if (player != null)
			return;
		if (other.tag == "Player") player = other.transform;
	}

	void OnTriggerExit(Collider other) {
		if (blocked)
			return;
		if (other.tag == "Player") player = null;
	}

	void OnCollisionEnter(Collision collision){
		if (blocked)
			return;
		if (collision.collider.name.Equals ("FireboltCollider")) {
			health -= 20;
			anim.SetTrigger ("isGettingDamaged");
			StartCoroutine (Wait (1f));
		} else if (collision.collider.name.Equals ("FlameStrikeCollider")) {
			health -= 40;
			anim.SetTrigger ("isGettingDamaged");
			StartCoroutine (Wait (1f));
		} 
		if (health <= 0) {
			collision.collider.gameObject.transform.parent.gameObject.GetComponentInChildren<SpellOwner> ().owner.GetComponentInChildren<QuestManager> ().SkeletonKilled ();
			collision.collider.gameObject.transform.parent.gameObject.GetComponentInChildren<SpellOwner> ().owner.GetComponentInChildren<SelectOnClick> ().Deselect ();
			this.gameObject.tag = "Untagged";
			damager.GetComponentInChildren<BoxCollider> ().enabled = false;
			int chance = UnityEngine.Random.Range (1, 10);
			if(chance >= 5) 
				collision.collider.gameObject.transform.parent.gameObject.GetComponentInChildren<SpellOwner> ().owner.GetComponentInChildren<QuestManager> ().AddGold (10);
		}
	}

	void Update () {
		if (blocked)
			return;
		
		if (health <= 0) {
			player = null;
			anim.SetBool ("isDead", true);
			StartCoroutine (WaitAndRevive (10f));
		}

		if (player != null) {
            anim.SetBool("isIdle", false);
            anim.SetBool("isRunning", false);
            if (Vector3.Distance(player.position, this.transform.position) > 5)
            {
                nav.speed = 2f;
                nav.SetDestination(player.position);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", true);
            }
        }
        else
        {
			if (!anim.GetBool ("isDead")) {
				if (Vector3.Distance (skeletonDefaultPosition, this.transform.position) > 2) {
					anim.SetBool ("isIdle", false);
					anim.SetBool ("isWalking", false);
					anim.SetBool ("isAttacking", false);
					anim.SetBool ("isRunning", true);
					nav.speed = 3f;
					nav.SetDestination (skeletonDefaultPosition);
				} else {
					anim.SetBool ("isIdle", true);
					anim.SetBool ("isWalking", false);
					anim.SetBool ("isAttacking", false);
					anim.SetBool ("isRunning", false);
				}
			}
        }
    }

	IEnumerator Wait(float numberOfSeconds) {

		blocked = true;
		nav.speed = 0f;

		yield return new WaitForSeconds (numberOfSeconds);

		blocked = false;
	}

	IEnumerator WaitAndRevive(float numberOfSeconds) {

		blocked = true;
		nav.speed = 0f;

		yield return new WaitForSeconds (numberOfSeconds);

		blocked = false;
		anim.SetBool ("isDead", false);
		health = 100;
		this.gameObject.tag = "Skeleton";
		damager.GetComponentInChildren<BoxCollider> ().enabled = true;
		StartCoroutine (Wait (1));
	}
}

