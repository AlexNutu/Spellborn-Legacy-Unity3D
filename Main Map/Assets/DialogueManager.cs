using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public GameObject dialogBox;
	public Text dialogText;
	private Button[] buttons;

	void Start () {
		dialogBox.SetActive (false);
		buttons = dialogBox.GetComponentsInChildren<Button> ();
	}

	public void disableBox(){
		dialogBox.SetActive (false);
	}
	
	public void ShowDialogue(string message, bool questReceived){
		dialogBox.SetActive (true);
		dialogText.text = message;
		if (questReceived) {
			buttons [0].gameObject.SetActive (true);
			buttons [1].gameObject.SetActive (false);
			buttons [2].gameObject.SetActive (false);
		} else {
			buttons [0].gameObject.SetActive (false);
			buttons [1].gameObject.SetActive (true);
			buttons [2].gameObject.SetActive (true);
		}
	}
}
