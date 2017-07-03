using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementFriendlyNPC : MonoBehaviour {

	int val = 0;
	float time;
	Animator anim;
	UnityEngine.AI.NavMeshAgent nav;
	private Vector3 villagerDefaultPosition, villagerModifiedPosition;
	// Use this for initialization
	void Start () {
		time = Random.Range(18f, 25f);
		anim = GetComponent<Animator>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		nav.speed = 1.5f;
		villagerDefaultPosition = this.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if(time > 0) time -= Time.deltaTime;
		else {
			if(val % 2 == 0) {  villagerModifiedPosition = this.transform.position + new Vector3(Random.Range(-7f, 7f), 0, Random.Range(-15f, 15f)); nav.SetDestination(villagerModifiedPosition); val++; time = Random.Range(18f, 25f); }
			else { nav.SetDestination(villagerDefaultPosition); val++; time = Random.Range(18f, 25f); }
		}
	}
}
