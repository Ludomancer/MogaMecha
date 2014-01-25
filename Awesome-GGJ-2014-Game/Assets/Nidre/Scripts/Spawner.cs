using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
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
        Instantiate(uberAwesomeMogaMecha, _pos, Quaternion.identity);
    }
}
