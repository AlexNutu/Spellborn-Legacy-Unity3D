using UnityEngine;
using System.Collections;

public class SpellCast : MonoBehaviour {

	public GameObject[] spell;
	public Transform initLocation;
	private Transform player;
	public Camera camera;
	private bool blocked;

	void Start() {
		blocked = false;
		player = this.transform;
	}

	void Update () {
		
		if (blocked)
			return;
		if (Input.GetKeyDown (KeyCode.N)) {
			StartCoroutine (Wait (1.5f));
			StartCoroutine (Cast (0.5f, 0, initLocation.position, initLocation.rotation));
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			StartCoroutine (Wait (3f));
			if (this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject) {
				Vector3 castLocation = this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject.transform.position;
				if (Vector3.Distance (this.transform.position, castLocation) <= 25) {
					StartCoroutine (Cast (0.5f, 1, castLocation, initLocation.rotation));
				}
			}
		}
	}

	IEnumerator Wait(float numberOfSeconds){

		blocked = true;

		yield return new WaitForSeconds (numberOfSeconds);

		blocked = false;
	}

	IEnumerator Cast(float numberOfSeconds, int spellID, Vector3 castLocation, Quaternion castRotation){

		blocked = true;

		yield return new WaitForSeconds (numberOfSeconds);

		GameObject instantiatedSpell = (GameObject)Instantiate (spell [spellID], castLocation, castRotation);
		instantiatedSpell.GetComponent<SpellOwner> ().owner = this.gameObject;
	}

	public void Lock(){
		blocked = true;
	}

	public void Unlock(){
		blocked = false;
	}
}