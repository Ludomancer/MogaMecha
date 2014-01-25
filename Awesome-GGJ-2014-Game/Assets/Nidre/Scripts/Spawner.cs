using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    List<GameObject> goList = new List<GameObject>();
    public GameObject uberAwesomeMogaMecha;
    Networking nt;
    public bool autoSpawn = true;
    public uberAwesomeMogaMecha.Style _bigBadWolf;
    void Start()
    {
        nt = GameObject.Find("Networking").GetComponent<Networking>();
        if (autoSpawn)
            InvokeRepeating("Spawn", 1, 7);
    }

    void Spawn()
    {
        _bigBadWolf = (uberAwesomeMogaMecha.Style)Random.Range(0, 5);
        print((int)_bigBadWolf);
        nt.SendBigBadWold((int)_bigBadWolf);
        Vector3 _pos;
        for (int i = 0; i < 5; i++)
        {
            _pos = transform.position;
            _pos += Vector3.right * Random.Range(-25f, 25f) + Vector3.forward * Random.Range(-5f, 5f);
            goList.Add(Instantiate(uberAwesomeMogaMecha, _pos, Quaternion.identity) as GameObject);
            nt.SpawnNewEnemy(goList[goList.Count - 1].transform.position, goList[goList.Count - 1].GetComponent<uberAwesomeMogaMecha>().id, (int)goList[goList.Count - 1].GetComponent<uberAwesomeMogaMecha>()._style);
        }
    }

    public void Spawn(Vector3 pos, int id, uberAwesomeMogaMecha.Style _style)
    {
        goList.Add(Instantiate(uberAwesomeMogaMecha, pos, Quaternion.identity) as GameObject);
        goList[goList.Count - 1].GetComponent<uberAwesomeMogaMecha>().id = id;
        goList[goList.Count - 1].GetComponent<uberAwesomeMogaMecha>().SetStyleAndBadWolf(_style);
    }

    public void RemoveObject(GameObject go)
    {
        goList.Remove(go);
    }

    public void Kill(int id)
    {
        for (int i = 0; i < goList.Count; i++)
        {
            if (goList[i].GetComponent<uberAwesomeMogaMecha>().id == id)
            {
                goList.Remove(goList[i]);
                Destroy(goList[i]);
            }
        }
    }
}
