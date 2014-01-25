using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    List<GameObject> goList = new List<GameObject>();
    public GameObject uberAwesomeMogaMecha;
    Networking nt;
    public bool autoSpawn = true;
    void Start()
    {
        nt = GameObject.Find("Networking").GetComponent<Networking>();
        if (autoSpawn)
            InvokeRepeating("Spawn", 1, 2);
    }

    void Spawn()
    {
        Vector3 _pos;
        _pos = transform.position;
        _pos += Vector3.right * Random.Range(-25f, 25f);
        goList.Add(Instantiate(uberAwesomeMogaMecha, _pos, Quaternion.identity) as GameObject);
        print(goList[goList.Count - 1].transform.position);
        nt.SpawnNewEnemy(goList[goList.Count - 1].transform.position, goList[goList.Count - 1].GetComponent<uberAwesomeMogaMecha>().id);
    }

    public void Spawn(Vector3 pos, int id)
    {
        goList.Add(Instantiate(uberAwesomeMogaMecha, pos, Quaternion.identity) as GameObject);
        goList[goList.Count - 1].GetComponent<uberAwesomeMogaMecha>().id = id;
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
