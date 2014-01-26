//#define MOUSE_CONTROL

using UnityEngine;
using System.Collections;

public class PunchingBag : MonoBehaviour
{
    Networking nt;
    float lastShot;
    private float interval = 1f;
    private float doubleInterval = 0.25f;
    private float currentInterval;

    public float CurrentInterval
    {
        get { return currentInterval; }
        set 
        {
            if (currentInterval > 0)
            {
                _mechaController.animation["Left Shot"].speed *= currentInterval / value;
                _mechaController.animation["Right Shot"].speed *= currentInterval / value;
            }
            currentInterval = value;
        }
    }
    float startWait;
    bool isDouble;
    bool shotSuccess = false;
    public float force = 3000;
    UnityHand h1;
    UnityHand h2;
    Renderer ind1;
    Renderer ind2;
    UpdateMechaRotation _mechaController;
    bool isFirstHand;

    public Material ok;
    public Material wait;
    public Material uber;

    public int availableLeftShell;
    public int availableRightShell;

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
        _mechaController = GameObject.Find("Mecha").GetComponent<UpdateMechaRotation>();

        interval = _mechaController.animation.GetClip("Left Shot").length;
    }

    void Update()
    {
        if (!_mechaController.isReady)
        {
#if MOUSE_CONTROL
            if (Input.GetMouseButtonDown(2))
            {
                _mechaController.Startup();
            }

            return;
#else
            if (canFire(h1) && canFire(h2))
            {
                startWait += Time.deltaTime;
                if (startWait > 1) _mechaController.Startup();
            }
            return;
#endif
        }
        startWait += Time.deltaTime;
        if (Time.realtimeSinceStartup - lastShot < currentInterval || startWait < 5) return;
        //print(canFire(h1) + " : " + canFire(h2));
        shotSuccess = false;
        //availableRightShell = availableLeftShell = 5;
        if (availableRightShell > 0)
        {
            ind1.sharedMaterial = ok;
        }
        else
        {
            ind1.sharedMaterial = wait;
        }
        if (availableLeftShell > 0)
        {
            ind2.sharedMaterial = ok;
        }
        else
        {
            ind2.sharedMaterial = wait;
        }

#if MOUSE_CONTROL
        if (Input.GetMouseButtonDown(0))
        {
            availableRightShell--;
            nt.RemoveRightShell();
            _mechaController.RightShot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            availableLeftShell--;
            nt.RemoveLeftShell();
            _mechaController.LeftShot();
        }

        return;
#endif

        if (startWait > 10 && canFire(h1) && canFire(h2))
        {
            CurrentInterval = doubleInterval;
            if (isFirstHand)
            {
                availableRightShell--;
                nt.RemoveRightShell();
                _mechaController.RightShot();
            }
            else
            {
                availableLeftShell--;
                nt.RemoveLeftShell();
                _mechaController.LeftShot();
            }
            isFirstHand = !isFirstHand;
             
            //    Double();
            //ind2.sharedMaterial = uber;
            //ind1.sharedMaterial = uber;
        }
        else
        {
            CurrentInterval = interval;
            if (canFire(h2) && availableRightShell > 0)
            {
                availableRightShell--;
                nt.RemoveRightShell();
                //Shoot(h2.transform.position);
                _mechaController.RightShot();
                isFirstHand = false;
            }
            else if (canFire(h1) && availableLeftShell > 0)
            {
                availableLeftShell--;
                nt.RemoveLeftShell();
                //Shoot(h1.transform.position);
                _mechaController.LeftShot();
                isFirstHand = true;
            }
        }
        lastShot = Time.realtimeSinceStartup;
    }


    bool isHandExists(UnityHand h)
    {
        return h.handFound;
    }

    bool isHandClosed(UnityHand h)
    {
        //print(h.leapFingers.Count + " : " + h.unityFingers.Count);
        return (h.leapFingers.Count < 2);
    }

    bool canFire(UnityHand h)
    {
        return isHandExists(h);
        print("Closed : " + isHandClosed(h));
        print("Exists : " + isHandExists(h));
        return isHandClosed(h) && isHandExists(h);
    }

    public void ShootMentosFromServer(Vector3[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            //Shoot(positions[i]);
        }
    }
}
