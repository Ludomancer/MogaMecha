using UnityEngine;
using System.Collections;

public class PunchingBag : MonoBehaviour
{
    Networking nt;
    public GameObject mentos;
    GameObject mentosCopy;
    float lastShot;
    float interval = 0.25f;
    float doubleInterval = 1.5f;
    float doubleStartTime;
    bool isDouble;
    bool shotSuccess = false;
    public float force = 3000;
    UnityHand h1;
    UnityHand h2;
    Renderer ind1;
    Renderer ind2;
    bool isFirstHand;

    public Material ok;
    public Material wait;
    public Material uber;

    //public void OnCollisionEnter(Collision collision)
    //{
    //    isDouble = (Time.realtimeSinceStartup - lastShot < doubleInterval);
    //    if (!isDouble && Time.realtimeSinceStartup - lastShot < interval) return;
    //    LeapBoxingObject leapObj = collision.gameObject.GetComponent<LeapBoxingObject>();

    //    if (leapObj)
    //    {
    //        //Debug.Log("mang : " + leapObj.maxVelocity.magnitude);
    //        //Debug.Log("velo : " + leapObj.maxVelocity);
    //        //Debug.Log("norm : " + leapObj.maxVelocity.normalized);
    //        //Debug.Log("dir : " + (collision.transform.position - transform.position));
    //        //if (leapObj.maxVelocity.z > 0) return;
    //        //Debug.Log(leapObj.maxVelocity.magnitude);
    //        Vector3 initPos = (Vector3.up * 2) +  (Camera.main.transform.forward * 5);
    //        mentosCopy = Instantiate(mentos, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
    //        mentosCopy.transform.position += initPos;
    //        mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
    //        if (isDouble)
    //        {
    //            mentosCopy = Instantiate(mentos, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
    //            mentosCopy.transform.position += (Camera.main.transform.right * 3) + initPos;
    //            mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
    //            mentosCopy = Instantiate(mentos, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
    //            mentosCopy.transform.position += -(Camera.main.transform.right * 3) + initPos;
    //            mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
    //        }
    //        lastShot = Time.realtimeSinceStartup;
    //    }
    //}

    void Start()
    {
        LeapHandController hc = GameObject.Find("Leap Controller Multiple/_leapController").GetComponent<LeapHandController>();
        ind1 = GameObject.Find("OVRCameraController/CameraLeft/h1Ind").renderer;
        ind2 = GameObject.Find("OVRCameraController/CameraRight/h2Ind").renderer;
        nt = GameObject.Find("Networking").GetComponent<Networking>();
        h1 = hc.unityHands[0];
        h2 = hc.unityHands[1];
    }

    void Update()
    {
        if (Time.realtimeSinceStartup - lastShot < interval) return;
        //print(canFire(h1) + " : " + canFire(h2));
        shotSuccess = false;

        if (isFirstHand)
        {
            ind1.sharedMaterial = ok;
            ind2.sharedMaterial = wait;
        }
        else
        {
            ind2.sharedMaterial = ok;
            ind1.sharedMaterial = wait;
        }

        if (canFire(h1) && canFire(h2))
        {
            Double();
            ind2.sharedMaterial = uber;
            ind1.sharedMaterial = uber;
        }
        else
        {
            doubleStartTime = 0;
            if (isFirstHand == true)
            {
                if (canFire(h1))
                {
                    Shoot(h1.transform.position);
                    isFirstHand = false;
                }
            }
            else
            {
                if (canFire(h2))
                {
                    Shoot(h2.transform.position);
                    isFirstHand = true;
                }
            }
        }
        lastShot = Time.realtimeSinceStartup;
    }

    private void Double()
    {
        if (doubleStartTime == 0)
        {
            doubleStartTime = Time.realtimeSinceStartup;
        }
        if (Time.realtimeSinceStartup - doubleStartTime > doubleInterval)
        {
            Shotgun(h1.transform.position);
            doubleStartTime = 0;
        }
    }

    void Shoot(Vector3 pos)
    {
        Vector3 initPos = (Vector3.up * 2) + (Camera.main.transform.forward * 5);
        mentosCopy = Instantiate(mentos, pos, Quaternion.identity) as GameObject;
        mentosCopy.transform.position += initPos;
        mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
        shotSuccess = true;
        nt.SendMentosPosition(new Vector3[] { mentosCopy.transform.position, Camera.main.transform.forward * force });
    }

    void Shotgun(Vector3 pos)
    {
        Vector3[] array = new Vector3[6];
        Vector3 initPos = (Vector3.up * 2) + (Camera.main.transform.forward * 5);
        mentosCopy = Instantiate(mentos, pos, Quaternion.identity) as GameObject;
        mentosCopy.transform.position += initPos;
        array[0] = mentosCopy.transform.position;
        array[1] = Camera.main.transform.forward * force;
        mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);

        mentosCopy = Instantiate(mentos, pos, Quaternion.identity) as GameObject;
        mentosCopy.transform.position += (Camera.main.transform.right * 3) + initPos;
        array[2] = mentosCopy.transform.position;
        array[3] = Camera.main.transform.forward * force;
        mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
        mentosCopy = Instantiate(mentos, pos, Quaternion.identity) as GameObject;
        mentosCopy.transform.position += -(Camera.main.transform.right * 3) + initPos;
        array[4] = mentosCopy.transform.position;
        array[5] = Camera.main.transform.forward * force;
        mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
        shotSuccess = true;

        nt.SendMentosPosition(array);
    }

    bool isHandExists(UnityHand h)
    {
        return h.handFound;
    }

    bool isHandClosed(UnityHand h)
    {
        return (h.leapFingers.Count < 2);
    }

    bool canFire(UnityHand h)
    {
        return isHandClosed(h) && isHandExists(h);
    }

    public void ShootMentosFromServer(Vector3[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            Shoot(positions[i]);
        }
    }
}
