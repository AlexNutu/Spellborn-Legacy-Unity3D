using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour {

    public Text nameTag;

	void Start () {
        GameObject.Find("nameTag").GetComponent<Text>().enabled = false;
    }
	
	void Update () {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled == true)
        {
            Vector3 tagPosition = Camera.main.WorldToScreenPoint(GameObject.Find("Sphere").transform.position);
            nameTag.transform.position = tagPosition;
            GameObject.Find("nameTag").GetComponent<Text>().enabled = true;
        }
    }   
}
