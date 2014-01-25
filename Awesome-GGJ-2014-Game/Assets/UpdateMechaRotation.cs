using UnityEngine;
using System.Collections;

public class UpdateMechaRotation : MonoBehaviour
{

    Animation anim;
    public GameObject mentos;
    GameObject mentosCopy;
    Networking nt;
    public Transform leftWeapon;
    public Transform rightWeapon;
    public Transform leftShoulder;
    public Transform rightShoulder;
    public float force;
    public bool isReady;

    void Start()
    {
        anim = animation;
        nt = GameObject.Find("Networking").GetComponent<Networking>();
    }

    public void Startup()
    {
        StartCoroutine("StartupRoutine");
    }

    IEnumerator StartupRoutine()
    {
        anim.Play("Startup");
        yield return new WaitForSeconds(anim.GetClip("Startup").length);
        isReady = true;
    }

    public void LeftShot()
    {
        mentosCopy = Instantiate(mentos, leftWeapon.position + leftWeapon.forward * 0.25f, Quaternion.identity) as GameObject;
        mentosCopy.rigidbody.AddForce(leftWeapon.forward * force);
        nt.SendMentosPosition(new Vector3[] { mentosCopy.transform.position, leftWeapon.forward * force });
        anim.Blend("Left Shot");
    }

    //void Shotgun(Vector3 pos)
    //{
    //    Vector3[] array = new Vector3[6];
    //    Vector3 initPos = (Camera.main.transform.forward * 5);
    //    mentosCopy = Instantiate(mentos, pos, Quaternion.identity) as GameObject;
    //    mentosCopy.transform.position += initPos;
    //    array[0] = mentosCopy.transform.position;
    //    array[1] = Camera.main.transform.forward * force;
    //    mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);

    //    mentosCopy = Instantiate(mentos, pos, Quaternion.identity) as GameObject;
    //    mentosCopy.transform.position += (Camera.main.transform.right * 3) + initPos;
    //    array[2] = mentosCopy.transform.position;
    //    array[3] = Camera.main.transform.forward * force;
    //    mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
    //    mentosCopy = Instantiate(mentos, pos, Quaternion.identity) as GameObject;
    //    mentosCopy.transform.position += -(Camera.main.transform.right * 3) + initPos;
    //    array[4] = mentosCopy.transform.position;
    //    array[5] = Camera.main.transform.forward * force;
    //    mentosCopy.rigidbody.AddForce(Camera.main.transform.forward * force);
    //    shotSuccess = true;

    //    nt.SendMentosPosition(array);
    //}

    public void RightShot()
    {
        mentosCopy = Instantiate(mentos, rightWeapon.position + rightWeapon.forward * 0.25f, Quaternion.identity) as GameObject;
        mentosCopy.rigidbody.AddForce(rightWeapon.forward * force);
        nt.SendMentosPosition(new Vector3[] { mentosCopy.transform.position, rightWeapon.forward * force });
        anim.Blend("Right Shot");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        return;
        Quaternion targetEuler = Quaternion.Euler(360 - Camera.main.transform.rotation.eulerAngles.x, leftShoulder.transform.rotation.eulerAngles.y, leftShoulder.transform.rotation.eulerAngles.z);
        leftShoulder.rotation = Quaternion.Lerp(leftShoulder.rotation, targetEuler, Time.deltaTime *2);
        rightShoulder.rotation = Quaternion.Lerp(leftShoulder.rotation, targetEuler, Time.deltaTime*2);
    }
}
