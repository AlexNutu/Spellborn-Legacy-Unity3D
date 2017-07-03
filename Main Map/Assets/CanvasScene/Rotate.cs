using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform rTrans = (RectTransform)transform.GetComponent<RectTransform>();


        rTrans.sizeDelta = new Vector2(Screen.width - 50 , Screen.width - 50);


    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime*5));
	}
}
