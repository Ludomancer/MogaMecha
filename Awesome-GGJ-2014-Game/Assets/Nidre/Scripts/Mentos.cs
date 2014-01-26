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

    void OnEnable()
    {
        StartCoroutine("TimeOut");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.name.Equals("uberEnemy"))
            Destroy(gameObject);
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }

}
