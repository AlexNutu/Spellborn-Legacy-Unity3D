using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class characterController : MonoBehaviour {

    // Use this for initialization
    static Animator anim;
    public float speed = 100.0F;
	NavMeshAgent nav;

	void Start () {
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;// pentru a face cursorul sa dispara din gameplay
		nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime; // Time.deltaTime = timpul ce a trecut de la ultimul update


        //translation= pt movement pe axa z, straffe = pt movement pt axa x
        straffe *= Time.deltaTime;

		nav.SetDestination(new Vector3(0, 0, translation) + this.transform.position);
        //transform.Rotate(0, straffe, 0);
        if (Input.GetButton("Fire1"))
        {
            anim.SetBool("isAttacking", true);
        }
        else
            anim.SetBool("isAttacking", false);

        if(translation != 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
        }
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
	}
}
