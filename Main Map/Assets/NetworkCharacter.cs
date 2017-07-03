using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;

    Animator anim;

	void Start () {
        anim = this.GetComponentInChildren<Animator>();
    }
	
	void Update () {
        
        if (photonView.isMine) {
               
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        
            stream.SendNext(anim.GetBool("isRunning"));
            stream.SendNext(anim.GetBool("isIdle"));
            stream.SendNext(anim.GetBool("isRunningBack"));
            stream.SendNext(anim.GetBool("isDead"));
            stream.SendNext(anim.GetBool("isJumping"));
            stream.SendNext(anim.GetBool("isCastingFireball"));
            stream.SendNext(anim.GetBool("isTurningLeft"));
            stream.SendNext(anim.GetBool("isTurningRight"));
            stream.SendNext(anim.GetBool("isCastingFireball"));
        }
        else {
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();

            anim.SetBool("isRunning", (bool)stream.ReceiveNext());
            anim.SetBool("isIdle", (bool)stream.ReceiveNext());
            anim.SetBool("isRunningBack", (bool)stream.ReceiveNext());
            anim.SetBool("isDead", (bool)stream.ReceiveNext());
            anim.SetBool("isJumping", (bool)stream.ReceiveNext());
            anim.SetBool("isCastingFireball", (bool)stream.ReceiveNext());
            anim.SetBool("isTurningLeft", (bool)stream.ReceiveNext());
            anim.SetBool("isTurningRight", (bool)stream.ReceiveNext());
            anim.SetBool("isCastingFireball", (bool)stream.ReceiveNext());
        }
    }
}
