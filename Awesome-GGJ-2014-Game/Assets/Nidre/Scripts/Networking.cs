using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour
{
    NetworkView nw;
    // Use this for initialization
    void Start()
    {
        nw = GetComponent<NetworkView>();
        LaunchServer();

        Network.Connect("10.0.27.129", 80);

        nw.RPC("PrintText", RPCMode.All, "Osman");
    }

    // Update is called once per frame
    void LaunchServer()
    {
        Network.incomingPassword = "HolyMoly";
        var useNat = !Network.HavePublicAddress();
        Network.InitializeServer(32, 80, useNat);
    }

    [RPC]
    void PrintText (string test)
    {
        Debug.Log(test);
    }
}
