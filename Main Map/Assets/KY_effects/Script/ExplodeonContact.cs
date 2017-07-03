using UnityEngine;
using System.Collections;

public class ExplodeonContact : MonoBehaviour {

	public GameObject explosion; 
	
	void OnCollisionEnter(Collision col){

		print("ayy lmao");
		GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		Destroy(gameObject); 
		Destroy(expl, 3); 
	}

}
