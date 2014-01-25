using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour
{

    TextMesh _left;
    TextMesh _right;
    TextMesh _leftHave;
    TextMesh _rightHave;

    int left;
    int right;
    public int leftHave;
    public int rightHave;

    int max = 10;

    NetworkingServer ns;

    // Use this for initialization
    void Start()
    {
        _left = Camera.main.transform.FindChild("Left").GetComponent<TextMesh>();
        _right = Camera.main.transform.FindChild("Right").GetComponent<TextMesh>();
        _leftHave = Camera.main.transform.FindChild("Left Have").GetComponent<TextMesh>();
        _rightHave = Camera.main.transform.FindChild("Right Have").GetComponent<TextMesh>();
        ns = GameObject.Find("Networking").GetComponent<NetworkingServer>();
        left = right = max;
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 200, 25, 200, 25), "Right Shell"))
        {
            right--;
            if (right == 0)
            {
                right = max;
                ns.AddRightShell();
                rightHave++;
                if (rightHave >= max)
                {
                    rightHave = max;
                    _rightHave.color = Color.green;
                }
                else _rightHave.color = Color.yellow;
                _rightHave.text = rightHave.ToString();
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
                leftHave++;
                if (leftHave >= max)
                {
                    leftHave = max;
                    _leftHave.color = Color.green;
                }
                else _leftHave.color = Color.yellow;
                _leftHave.text = leftHave.ToString();
            }
            _left.text = left.ToString();

        }
    }
}
