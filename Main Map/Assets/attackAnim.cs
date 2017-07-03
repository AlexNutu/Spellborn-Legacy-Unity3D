using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackAnim : MonoBehaviour
{

    // Use this for initialization
    static Animator anim;
    public Transform target;
    public Transform GameCamera;
    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    static int delay = 0;
    public float timer = 4.0F;
    public float timerMax = 0.2F;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isAttacking", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //if(timer > timerMax-0.1F)
            anim.SetBool("isAttacking", true);

            if (timer > timerMax + 0.05) // necesara pentru atunci cand userul apasa click foarte des
                timer = 0.0F;

        }
        else if (timer > timerMax)
        {
            anim.SetBool("isAttacking", false);
        }
    }
}