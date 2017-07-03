using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOnClick : MonoBehaviour {

	public Camera camera;
	public GameObject selectedObject;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) {
				if (hitInfo.transform.gameObject.tag == "Player" || hitInfo.transform.gameObject.tag == "Crate" ||
				    hitInfo.transform.gameObject.tag == "NPC" || hitInfo.transform.gameObject.tag == "Skeleton") {
					selectedObject = hitInfo.transform.gameObject;
				} else {
					selectedObject = null;
				}
			} else {
				selectedObject = null;
			}
		} 
	}

	public void Deselect(){
		selectedObject = null;
	}
}
