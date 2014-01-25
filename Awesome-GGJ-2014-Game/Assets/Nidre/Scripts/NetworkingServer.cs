using UnityEngine;
using System.Collections;

public class NetworkingServer : MonoBehaviour
{
    NetworkView nw;
    public PunchingBagServer _pb;
	bool isServer;
    // Use this for initialization
    void Start()
    {
        nw = GetComponent<NetworkView>();
        LaunchServer();
        //NetworkConnectionError ne = Network.Connect("10.0.27.129", 80);

        //Debug.Log(ne);
    }

    // Update is called once per frame
    void LaunchServer()
    {
        var useNat = !Network.HavePublicAddress();
        Network.InitializeServer(32, 80, false);
    }

    void OnServerInitialized()
    {
		isServer = true;
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
		if (isServer) return;
        nw.RPC("GetMentosPosition", RPCMode.All, positions);
    }
	 [RPC]
    public void SendDeadEnemyId(params int[] ids)
    {
		if (isServer) return;
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
    public void GetMentosPosition(string data)
    {
        Debug.Log("Recieved Mentos");
		//print (data);
		string[] split = data.Split('*');
        Vector3[] p = new Vector3[split.Length - 1];
        for (int i = 0; i < p.Length; i++)
        {
			//print (split[i]);
            string[] vector3 = split[i].Split(',');
			//for (int y = 0; y < vector3.Length; y++) 
			//{
			//	print (y + " : " + vector3[y]);
			//}
            //print(vector3[0] + " : " + vector3[1] + " : " + vector3[2]);
            p[i] = new Vector3(float.Parse(vector3[0]), float.Parse(vector3[1]), float.Parse(vector3[2]));
            //print(p[i]);
        }
        _pb.ShootMentosFromServer(p);
    }
}
