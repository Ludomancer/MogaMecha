using UnityEngine;
using System.Collections;

public class Networking : MonoBehaviour
{
    Load _loader;
    NetworkView nw;
    public PunchingBag _pb;
    public bool isConnected;
    public string url = "10.0.27.129";
    public int port = 80;
    // Use this for initialization
    void Start()
    {
        nw = GetComponent<NetworkView>();
        _loader = Camera.main.GetComponent<Load>();
    }

    void OnGUI()
    {
        if (!isConnected && GUI.Button(new Rect(10, 10, 200, 50), "Connect"))
        {
            Debug.Log("Connect");
            NetworkConnectionError ne = Network.Connect(url, port);
        }
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
        isConnected = true;
        Debug.Log("Connected");
    }

    void OnFailedToConnect()
    {
        isConnected = false;
        Debug.Log("Zic");
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        isConnected = false;
        Debug.Log("Disconnected " + info.ToString());
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + " connected from " + player.ipAddress);
    }

    void OnPlayerDisConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + " disconnected from " + player.ipAddress);
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
    [RPC]
    public void AddLeftShell()
    {
        _pb.availableLeftShell++;
    }
    [RPC]
    public void AddRightShell()
    {
        _pb.availableRightShell++;
    }

    [RPC]
    public void RemoveLeftShell()
    {
        nw.RPC("RemoveLeftShell", RPCMode.Others);
    }
    [RPC]
    public void RemoveRightShell()
    {
        nw.RPC("RemoveRightShell", RPCMode.Others);
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
        print("SEND : " + ((uberAwesomeMogaMecha.Style)wolf).ToString());
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
