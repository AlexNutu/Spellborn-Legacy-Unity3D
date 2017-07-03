using UnityEngine;
using System.Collections;

public class Destruction: MonoBehaviour {

	public GameObject explosion;
	private int health;

	void Start(){
		health = 100;
	}
	
	void OnCollisionEnter(Collision collision){
		if (collision.collider.name == "FlameStrikeCollider") {
			health -= 100;
		}
		if (collision.collider.name == "FireboltCollider") {
			health -= 51;
		}
		if (health <= 0) {
			this.GetComponent<MeshRenderer> ().enabled = false;
			this.GetComponent<BoxCollider> ().enabled = false;
			int chance = Random.Range (1, 10);
			if(chance >= 3) 
				collision.collider.gameObject.transform.parent.gameObject.GetComponentInChildren<SpellOwner> ().owner.GetComponentInChildren<QuestManager> ().AddGold (5);
			GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
			Destroy(expl, 3); 
			collision.collider.gameObject.transform.parent.gameObject.GetComponentInChildren<SpellOwner> ().owner.GetComponentInChildren<QuestManager> ().BoxDestroyed ();
			StartCoroutine (WaitAndRebuild ());
		}
	}

	IEnumerator WaitAndRebuild(){

		yield return new WaitForSeconds (10);

		this.GetComponent<MeshRenderer> ().enabled = true;
		this.GetComponent<BoxCollider> ().enabled = true;
		health = 100;
	}
	

}
