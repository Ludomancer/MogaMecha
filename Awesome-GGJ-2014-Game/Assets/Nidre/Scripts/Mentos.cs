using UnityEngine;
using System.Collections;

public class Mentos : MonoBehaviour
{

    //// Use this for initialization
    //void OnEnable()
    //{
    //    StartCoroutine("Destroy");
    //}

    //IEnumerator Destroy()
    //{
    //    yield return new WaitForSeconds(3);
    //    Destroy(gameObject);
    //}

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
