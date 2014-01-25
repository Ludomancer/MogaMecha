using UnityEngine;
using System.Collections;

public class uberAwesomeMogaMecha : MonoBehaviour {

    public int id;
    Spawner spawner;

    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    void OnCollisionEnter(Collision collision)
    {
        spawner.RemoveObject(gameObject);
        Destroy(gameObject);
    }
}
