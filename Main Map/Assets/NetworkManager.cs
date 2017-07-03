using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{

    public Camera standbyCamera;
    GameObject spawnSpot;
    Camera cam;
    GameObject myPlayerGO;
	public Text username;
	public Text password;
	Text nametag;

	public void Ceva(){
		if (username.text == "Dan" || username.text == "Angelo" || username.text == "Alex" || username.text == "Catalin"
		    || username.text == "Stefan" || username.text == "Madalina") {
			if (password.text == "parola") {
				Connect ();
				nametag.text = username.text;
			}
		}
	}

    void Start()
    {
        spawnSpot = GameObject.FindGameObjectWithTag("SpawnSpot");
        //Connect();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("V3");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRoom("World");
    }


    void OnJoinedRoom()
    {
        Debug.Log("World Joined");
        standbyCamera.enabled = false;
        SpawnMyPlayer();
    }

    void SpawnMyPlayer()
    {
        if (spawnSpot == null)
        {
            Debug.LogError("WTF?!?!?");
            return;
        }
        GameObject mySpawnSpot = spawnSpot;
        myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Player", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
        cam = myPlayerGO.GetComponentInChildren<Camera>();
        cam.enabled = true;
        ((MonoBehaviour)myPlayerGO.GetComponent("playerContoller")).enabled = true;
        ((MonoBehaviour)myPlayerGO.GetComponent("SpellCast")).enabled = true;
        ((MonoBehaviour)myPlayerGO.GetComponent("QuestManager")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("SelectOnClick")).enabled = true;
		((MonoBehaviour)myPlayerGO.GetComponent("DetectHit")).enabled = true;
    }
}
