using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    List<GameObject> goList = new List<GameObject>();
    public GameObject uberAwesomeMogaMecha;
    void Start()
    {
        InvokeRepeating("Spawn", 1, 2);
    }

    void Spawn()
    {
        Vector3 _pos;
        _pos = transform.position;
        _pos += Vector3.right * Random.Range(-25f, 25f);
        goList.Add(Instantiate(uberAwesomeMogaMecha, _pos, Quaternion.identity) as GameObject);
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
