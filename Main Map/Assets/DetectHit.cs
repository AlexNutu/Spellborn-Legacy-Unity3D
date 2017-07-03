using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectHit : MonoBehaviour {

    public Slider healthbar;
	public int maxHP = 100;
    Animator anim;
	playerContoller controller;
	SpellCast caster;
	public GameObject respawnSpot;
	QuestManager questManager;
	private float time = 0f;

    void OnCollisionEnter(Collision other)
    {
		if (other.collider.gameObject.tag != "Smiter") return;

        healthbar.value -= 10;
        
        if (healthbar.value > 0)
        {
            //anim.SetBool("isReacting", true);
        }

        if (healthbar.value <= 0)
        {   
            anim.SetBool("isDead", true);
			questManager.FillAlert ("Respawning soon...");
			StartCoroutine (WaitAndRespawn ());
        }
    }

    
    // Use this for initialization
    void Start () {
		controller = this.GetComponentInChildren<playerContoller> ();
        anim = GetComponent<Animator>();
		caster = this.GetComponentInChildren<SpellCast> ();
		questManager = this.GetComponentInChildren<QuestManager> ();
    }
	
	// Update is called once per frame
	void Update () {
		time += 0.05f;
		if (time >= 10) {
			time = 0;
			healthbar.value += 5;
			if (healthbar.value > maxHP) {
				healthbar.value = maxHP;
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			if (questManager.UsePotion ()) {
				healthbar.value += 20;
			}
		}
	}

	IEnumerator WaitAndRespawn(){
		controller.Lock ();
		caster.Lock ();

		yield return new WaitForSeconds (5);

		questManager.ClearAlert ();
		anim.SetBool ("isDead", false);
		controller.Unlock ();
		caster.Unlock ();
		healthbar.value = 100;
		this.transform.position = respawnSpot.transform.position;
	}
}
