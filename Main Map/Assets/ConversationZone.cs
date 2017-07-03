using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConversationZone : MonoBehaviour {

	private QuestManager questManager;
	public int allyIndex;
	private playerContoller controller;
	private SpellCast caster;
	private GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player" && other.gameObject == player) {
			if (Input.GetKeyDown (KeyCode.T)) {
				questManager.StartConversation (allyIndex);
				controller.Lock ();
				caster.Lock ();
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" && player == null) {
			player = other.gameObject;
			questManager = player.GetComponentInChildren<QuestManager> ();
			controller = player.GetComponentInChildren<playerContoller> ();
			caster = player.GetComponentInChildren<SpellCast> ();
			questManager.FillAlert ("Press 'T' to start conversation");
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player" && player != null) {
			questManager.ClearAlert ();
			player = null;
			controller = null;
			caster = null;
			questManager = null;
		}
	}
}
