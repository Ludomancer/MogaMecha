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
        nw.RPC("PrintText", RPCMode.Others, "Osman");
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
    public void SendMentosPosition(Vector3[] positions)
    {
        string stringOfAllFloats = "";
        for (int i = 0; i < positions.Length; i++)
        {
            stringOfAllFloats += positions[i].ToString() + "*";
        }
        stringOfAllFloats = stringOfAllFloats.Replace("(", "");
        stringOfAllFloats = stringOfAllFloats.Replace(")", "");
        nw.RPC("GetMentosPosition", RPCMode.Others, stringOfAllFloats);

    }

    [RPC]
    public void SendDeadEnemyId(params int[] ids)
    {
        string stringOfAllFloats = "";
        for (int i = 0; i < ids.Length; i++)
        {
            stringOfAllFloats += ids[i].ToString() + "*";
        }
        nw.RPC("GetDeadEnemyId", RPCMode.Others, ids);
    }

    public void AddLeftShell()
    {
        _pb.availableLeftShell++;
    }

    public void AddRightShell()
    {
        _pb.availableLeftShell--;
    }


    [RPC]
    public void SpawnNewEnemy(Vector3 position, int id, int style)
    {
        nw.RPC("GetAndSpawnNewEnemy", RPCMode.Others, position, id, style);
    }

    [RPC]
    public void GetAndSpawnNewEnemy(Vector3 position, int id, int style)
    {

    }

    [RPC]
    public void SendBigBadWold(int wolf)
    {
        nw.RPC("GetBigBadWold", RPCMode.Others, wolf);
    }

    [RPC]
    public void GetBigBadWold(int wolf)
    {

    }

    [RPC]
    public void GetDeadEnemyId(string ids)
    {
    }

    [RPC]
    public void GetMentosPosition(string data)
    {
    }

    // [RPC]
    //public void GetMentosPosition(Vector3[] positions)
    //{
    //    Debug.Log("Recieved Mentos");
    //    for (int i = 0; i < positions.Length; i++)
    //    {
    //        Debug.Log(positions[i]);
    //    }
    //    _pb.ShootMentosFromServer(positions);
    //}
}
