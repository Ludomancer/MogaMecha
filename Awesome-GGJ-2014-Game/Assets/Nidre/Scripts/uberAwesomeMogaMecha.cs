using UnityEngine;
using System.Collections;

public class uberAwesomeMogaMecha : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
