using UnityEngine;
using System.Collections;

public class ipEdit : MonoBehaviour {
    public string stringToEdit = "IPNumber";
	float positionX = 2.66f;
	void OnGUI() {
		if(GameObject.Find("Networking").GetComponent<Networking>().isConnected == false){
        stringToEdit = GUI.TextField(new Rect(((Screen.width/4)-100), Screen.height/2, 200, 20), stringToEdit, 25);
		stringToEdit = GUI.TextField(new Rect((((Screen.width/4)*positionX)-100), Screen.height/2, 200, 20), stringToEdit, 25);
    }}
}