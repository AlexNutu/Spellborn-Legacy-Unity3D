using System.Collections;
using UnityEngine;

public class playerContoller : MonoBehaviour {

	private Animator anim;
	private float speed = 8f;
	private float rotationSpeed = 70f;
	private Rigidbody rigidbody;
	private bool blocked;
	private SelectOnClick selected;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rigidbody = GetComponent<Rigidbody> ();
		selected = GetComponent<SelectOnClick> ();
		blocked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (blocked)
			return;
		float translation = Input.GetAxis ("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);

		if (Input.GetKey (KeyCode.E)) {
			speed = 12f;
		} else {
			speed = 8f;
		}

		if (Input.GetButtonDown ("Jump")) {
			anim.SetTrigger ("isJumping");
		}

		if (Input.GetKeyDown (KeyCode.N)){
			anim.SetTrigger ("isCastingFireball");
			StartCoroutine (PauseInput (1.6f));
		} 

		if (Input.GetKeyDown (KeyCode.M)){
			if (selected.selectedObject != null && Vector3.Distance (selected.selectedObject.transform.position, this.transform.position) <= 25) {
				anim.SetTrigger ("isCastingFirefall");
				StartCoroutine (PauseInput (2.968f));
			}
		} 
			
		if (Input.GetKey (KeyCode.W)) {
			anim.SetBool ("isRunning", true);
			anim.SetBool ("isRunningBack", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isTurningRight", false);
			anim.SetBool ("isTurningLeft", false);
		}
		if (Input.GetKey (KeyCode.S)) {
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isRunningBack", true);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isTurningRight", false);
			anim.SetBool ("isTurningLeft", false);
			speed = 5;
		}
				
		if(translation == 0) {
			if (Input.GetKey (KeyCode.A)) {
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isRunningBack", false);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isTurningRight", false);
				anim.SetBool ("isTurningLeft", true);
			} else if (Input.GetKey (KeyCode.D)) {
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isRunningBack", false);
				anim.SetBool ("isIdle", false);
				anim.SetBool ("isTurningRight", true);
				anim.SetBool ("isTurningLeft", false);
			} else {	
				anim.SetBool ("isRunning", false);
				anim.SetBool ("isRunningBack", false);
				anim.SetBool ("isIdle", true);
				anim.SetBool ("isTurningLeft", false);
				anim.SetBool ("isTurningRight", false);
			}
		}



		if (rotation == 0) {
			rigidbody.freezeRotation = true;
		}

		

		Vector3 rotationVector = transform.rotation.eulerAngles;
		rotationVector.x = 0;
		rotationVector.z = 0;
		transform.rotation = Quaternion.Euler (rotationVector);
	}

	IEnumerator PauseInput (float numberOfSeconds){

		blocked = true;

		yield return new WaitForSeconds (numberOfSeconds);

		blocked = false;
	}

	public void Lock(){
		blocked = true;
	}

	public void Unlock(){
		blocked = false;
	}
}
