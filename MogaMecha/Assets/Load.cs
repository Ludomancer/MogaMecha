using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour
{
    bool isLeft;
    bool isRight;

    TextMesh _left;
    TextMesh _right;
    TextMesh _leftHave;
    TextMesh _rightHave;
    TextMesh _player;

    int left;
    int right;
    private int leftHave;

    public int LeftHave
    {
        get { return leftHave; }
        set
        {
            _leftHave.text = value.ToString();
            leftHave = value;
        }
    }
    private int rightHave;

    public int RightHave
    {
        get { return rightHave; }
        set
        {
            _rightHave.text = value.ToString();
            rightHave = value;
        }
    }

    int max = 7;
    int maxAmmo = 10;

    NetworkingServer ns;

    // Use this for initialization
    void Start()
    {
        _left = Camera.main.transform.FindChild("Left").GetComponent<TextMesh>();
        _right = Camera.main.transform.FindChild("Right").GetComponent<TextMesh>();
        _leftHave = Camera.main.transform.FindChild("Left Have").GetComponent<TextMesh>();
        _rightHave = Camera.main.transform.FindChild("Right Have").GetComponent<TextMesh>();
        _player = Camera.main.transform.FindChild("Player").GetComponent<TextMesh>();
        ns = GameObject.Find("Networking").GetComponent<NetworkingServer>();
        left = right = max;
        _left.text = _right.text = max.ToString();
    }

    // Update is called once per frame
    void OnGUI()
    {
        isLeft = isRight = false;
        Rect r;

        r = new Rect(Screen.width - 200, 25, 200, 50);
        GUI.Button(r, "Right Shell");
        r = new Rect(Screen.width - 200, Screen.height - 75, 200, 50);
        if (r.Contains(Input.mousePosition))
        {
            isRight = true;

        }

        r = new Rect(25, 25, 200, 50);
        GUI.Button(r, "Left Shell");
        r = new Rect(25, Screen.height - 75, 200, 50);
        if (r.Contains(Input.mousePosition))
        {
            isLeft = true;

        }
    }

    public void PlayerStatusChanged(NetworkPlayer player, bool isLoggedIn)
    {
        if (isLoggedIn) _player.text = player.ipAddress + " has been connected.";
        else _player.text = player.ipAddress + " has been disconnected.";
        StartCoroutine("HidePlayerInfo");
    }

    IEnumerator HidePlayerInfo()
    {
        float val = 1;
        while (val > 0)
        {
            val -= Time.deltaTime;
            _player.color = new Color(1, 1, 1, val);
            yield return null;
        }
        _player.text = "";
        _player.color = new Color(1, 1, 1, 1);
    }

    void Update()
    {
        if (isRight)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                right--;
                if (right == 0)
                {
                    right = max;
                    ns.AddRightShell();
                    rightHave++;
                    if (rightHave >= maxAmmo)
                    {
                        rightHave = maxAmmo;
                        _rightHave.color = Color.green;
                    }
                    else _rightHave.color = Color.yellow;
                    _rightHave.text = rightHave.ToString();
                }
                _right.text = right.ToString();
            }
        }
        else if (isLeft)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                left--;
                if (left == 0)
                {
                    left = max;
                    ns.AddLeftShell();
                    leftHave++;
                    if (leftHave >= maxAmmo)
                    {
                        leftHave = maxAmmo;
                        _leftHave.color = Color.green;
                    }
                    else _leftHave.color = Color.yellow;
                    _leftHave.text = leftHave.ToString();
                }
                _left.text = left.ToString();
            }
        }
    }
}
