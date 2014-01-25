using UnityEngine;
using System.Collections;

public class BagSpawnerServer : MonoBehaviour
{

    public GameObject bag;
    private GameObject currentBag;
    public GameObject spawner;
    public OVRCameraController ovr;
    public Material ok;
    public Material wait;
    public Material uber;

    void Start()
    {
        BagManagerServer.bagSpawner = this;
        currentBag = (GameObject)Instantiate(bag);
        currentBag.transform.position = new Vector3(0, 8, -3);
        currentBag.transform.GetChild(0).GetComponent<PunchingBagServer>().ok = ok;
        currentBag.transform.GetChild(0).GetComponent<PunchingBagServer>().wait = wait;
        currentBag.transform.GetChild(0).GetComponent<PunchingBagServer>().uber = uber;
        GameObject.Find("Networking").GetComponent<NetworkingServer>()._pb = currentBag.transform.GetChild(0).GetComponent<PunchingBagServer>();
        //spawner.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1000;
    }

    public void RespawnNewBag()
    {
        Destroy(currentBag);
        currentBag = (GameObject)Instantiate(bag);
    }
}
