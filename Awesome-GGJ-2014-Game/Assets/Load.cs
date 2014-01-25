using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour {

    TextMesh _left;
    TextMesh _right;

    int left;
    int right;

    int max = 10;
	
	NetworkingServer ns;

	// Use this for initialization
	void Start () 
    {
        _left = Camera.main.transform.FindChild("Left").GetComponent<TextMesh>();
        _right = Camera.main.transform.FindChild("Right").GetComponent<TextMesh>();
		ns = GameObject.Find (	"Networking").GetComponent<NetworkingServer>();
        left = right = max;
	}
	
	// Update is called once per frame
	void OnGUI () 
    {
        if (GUI.Button(new Rect(Screen.width - 200, 25, 200, 25), "Right Shell"))
        {
            right--;
            if (right == 0)
            {
                right = max;
							ns.AddRightShell();
            }
            _right.text = right.ToString();

        }
        else if (GUI.Button(new Rect(25, 25, 200, 25), "Left Shell"))
        {
            left--;
            if (left == 0)
            {
                left = max;
										ns.AddLeftShell();
            }
            _left.text = left.ToString();

        }
	}
}
