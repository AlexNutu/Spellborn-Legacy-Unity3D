using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

	public GameObject questPanel;
	public GameObject dialogPanel;
	private int[] dialogProgress;
	public Text text;
	public Text alert;
	private Text[] questParts;
	public Button[] buttons;
	public Text tag;
	public Slider slider;
	private int playerGold;
	private int potions;
	private int currentConversation;
	private int skeletonsSlain;
	private int boxesDestroyed;
	private int experience;
	private int level;

	// Use this for initialization
	void Start () {
		questParts = questPanel.GetComponentsInChildren<Text> ();
		dialogProgress = new int[4];
		questPanel.SetActive (false);
		dialogPanel.SetActive (false);
		playerGold = 0;
		potions = 0;
		skeletonsSlain = 0;
		boxesDestroyed = 0;
		level = 1;
		experience = 0;
	}

	// Update is called once per frame
	void Update () {
		if (experience >= 20 * level) {
			experience = 0;
			level += 1;
			FillAlert ("Leveled up, now level " + level);
			WaitAndClear ();
		}

		if (Input.GetKey (KeyCode.Q)) {
			questPanel.SetActive (true);
		} else {
			questPanel.SetActive (false);
		}

		if (dialogProgress [3] == 1) {
			questParts [0].text = "Skeletons to be killed: " + (4 - skeletonsSlain);
		} else {
			questParts [0].text = "";
		}

		if (dialogProgress [0] == 1) {
			questParts [1].text = "Boxes to be destroyed: " + (4 - boxesDestroyed);
		} else {
			questParts [1].text = "";
		}

		questParts [2].text = "Level: " + level + "\nGold: " + playerGold + "\nPotions: " + potions;

		if (this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject != null) {
			tag.text = this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject.tag;
			if (this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject.tag == "Skeleton") {
				slider.gameObject.SetActive (true);
				slider.value = this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject.GetComponentInChildren<skeletonMovement> ().health;
			} else if (this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject.tag == "Player") {
				slider.gameObject.SetActive (true);
				slider.value = this.gameObject.GetComponentInChildren<SelectOnClick> ().selectedObject.GetComponentInChildren<DetectHit> ().healthbar.value;
			} else {
				slider.gameObject.SetActive (false);
			}
		} else {
			tag.text = "";
			slider.gameObject.SetActive (false);
		}
	}

	public void StartConversation(int allyIndex){
		this.gameObject.GetComponentInChildren<Animator> ().SetBool ("isIdle", true);
		this.gameObject.GetComponentInChildren<Animator> ().SetBool ("isRunning", false);
		alert.text = "";
		dialogPanel.SetActive (true);
		currentConversation = allyIndex;
		switch (allyIndex) {
		case 0:
			{
				if (dialogProgress [allyIndex] == 0) {
					text.text = "Hey, I was trying to do something illegal but since I'm dumb I almost got caught." +
					" I had to hide some boxes in the village but now I want them destroyed. Will you do it for me? I'm too scared to.";
					buttons [0].gameObject.SetActive (false);
					buttons [1].gameObject.SetActive (true);
					buttons [2].gameObject.SetActive (true);
				} else if (dialogProgress [allyIndex] == 1) {
					text.text = "You know what's in those boxes? ... Nah, I'm not going to tell you actually.";
					buttons [0].gameObject.SetActive (true);
					buttons [1].gameObject.SetActive (false);
					buttons [2].gameObject.SetActive (false);
				} else {
					text.text = "I got away thanks to you. I would give you money but I'm broke. I'm off to do more exciting stuff!";
					buttons [0].gameObject.SetActive (true);
					buttons [1].gameObject.SetActive (false);
					buttons [2].gameObject.SetActive (false);
				}
				break;
			}
		case 1:
			{
				text.text = "Hey, want a potion that will keep you refreshed? I have some right here, but only for 10 gold a piece." +
					" What do you say? Quick, before they come for me!";
				buttons [0].gameObject.SetActive (false);
				buttons [1].gameObject.SetActive (true);
				buttons [2].gameObject.SetActive (true);
				break;
			}
		case 2:
			{
				text.text = "This was a productive day, I made some improvements in my home, I build those fences over there, isn't" +
					" that neat? Some maniac was trying to destroy them last night, he was holding some boxes. Fishy stuff man...";
				buttons [0].gameObject.SetActive (true);
				buttons [1].gameObject.SetActive (false);
				buttons [2].gameObject.SetActive (false);
				break;
			}
		case 3:
			{
				if (dialogProgress [allyIndex] == 0) {
					text.text = "I'm working on a potion that would cure some bad disease. Quality stuff, not like that scoundrel" +
						" potion seller distributes to the unsuspecting folk. But I need skeleton bones. Can you get me some? I will give you" +
						" 40 gold if you can.";
					buttons [0].gameObject.SetActive (false);
					buttons [1].gameObject.SetActive (true);
					buttons [2].gameObject.SetActive (true);
				} else if (dialogProgress [allyIndex] == 1) {
					text.text = "I need those bones... Looking forward to better news. Isn't the sky nice?";
					buttons [0].gameObject.SetActive (true);
					buttons [1].gameObject.SetActive (false);
					buttons [2].gameObject.SetActive (false);
				} else {
					text.text = " Thanks, I'm brewing it. I also have plans for a new invention, revolutionary, let me tell you...";
					buttons [0].gameObject.SetActive (true);
					buttons [1].gameObject.SetActive (false);
					buttons [2].gameObject.SetActive (false);
				}
				break;
			}
		}
	}

	public void AddPotion(){
		if (currentConversation != 1)
			return;
		if (playerGold >= 10) {
			playerGold -= 10;
			potions++;
		} else {
			alert.text = "Not enough gold!";
			StartCoroutine(WaitAndClear ());
		}
	}

	public bool UsePotion(){
		if (potions == 0)
			return false;
		potions--;
		return true;
	}

	public void ClearAlert(){
		alert.text = "";
	}

	public void FillAlert(string message){
		alert.text = message;
	}

	public void IncrementProgress(){
		if (currentConversation == 1)
			return;
		dialogProgress [currentConversation]++;
	}

	public void AddGold(int goldAmount){
		alert.text = "Received " + goldAmount + " gold!";
		playerGold += goldAmount;
		StartCoroutine(WaitAndClear ());
	}

	public void SkeletonKilled(){
		experience += 10;
		if (dialogProgress [3] != 1)
			return;
		skeletonsSlain++;
		if (skeletonsSlain == 4) {
			dialogProgress [3]++;
			AddGold (40);
		}
	}

	public void BoxDestroyed(){
		experience += 5;
		if (dialogProgress [0] != 1)
			return;
		boxesDestroyed++;
		if (boxesDestroyed == 4) {
			dialogProgress [0]++;
		}
	}

	public void CloseDialogPanel(){
		dialogPanel.SetActive (false);
	}

	IEnumerator WaitAndClear(){

		yield return new WaitForSeconds (2);

		alert.text = "";
	}
}
