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

        NetworkConnectionError ne = Network.Connect("10.0.27.129", 80);

        Debug.Log(ne);

        nw.RPC("PrintText", RPCMode.All, "Osman");
    }

    // Update is called once per frame
    void LaunchServer()
    {
        var useNat = !Network.HavePublicAddress();
        Network.InitializeServer(32, 80, useNat);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized and ready");
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + " connected from " + player.ipAddress);
    }

    [RPC]
    void PrintText(string test)
    {
        Debug.Log(test);
    }
}
