using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour
{
    NetworkView nw;
    public PunchingBag _pb;
    // Use this for initialization
    void Start()
    {
        nw = GetComponent<NetworkView>();
        //LaunchServer();
        NetworkConnectionError ne = Network.Connect("10.0.27.129", 80);

        Debug.Log(ne);
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

    void OnConnectedToServer()
    {
        Debug.Log("Connected");
        nw.RPC("PrintText", RPCMode.All, "Osman");
    }

    void OnFailedToConnect()
    {
        Debug.Log("Zic");
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

     [RPC]
    public void SendMentosPosition(params Vector3[] positions)
    {
        nw.RPC("GetMentosPosition", RPCMode.All, positions);
    }

     [RPC]
    public void SendDeadEnemyId(params int[] ids)
    {
        nw.RPC("GetDeadEnemyId", RPCMode.All, ids);
    }

     [RPC]
    public void GetDeadEnemyId(params int[] ids)
    {
        Debug.Log("Recieved Enemy");
        for (int i = 0; i < ids.Length; i++)
        {
            Debug.Log(ids[i]);
        }
    }

     [RPC]
    public void GetMentosPosition(params Vector3[] positions)
    {
        Debug.Log("Recieved Mentos");
        for (int i = 0; i < positions.Length; i++)
        {
            Debug.Log(positions[i]);
        }
        _pb.ShootMentosFromServer(positions);
    }
}
