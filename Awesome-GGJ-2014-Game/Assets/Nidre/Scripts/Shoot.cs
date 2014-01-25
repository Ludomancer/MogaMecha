using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        print("BOK");
        LeapBoxingObject leapObj = collision.gameObject.GetComponent<LeapBoxingObject>();

        if (leapObj)
        {
            Debug.Log("HIT : " + leapObj.maxVelocity.magnitude);
            rigidbody.AddForceAtPosition(leapObj.maxVelocity * 800, leapObj.transform.position);
        }
    }
}
