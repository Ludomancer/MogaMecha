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
    private bool isPreparing;
    AudioSource ao;
    AudioSource startupSound;

    void Start()
    {
        anim = animation;
        nt = GameObject.Find("Networking").GetComponent<Networking>();
        ao = transform.FindChild("ShotSound").audio;
        startupSound = transform.FindChild("StartupSound").audio;
    }

    public void Startup()
    {
        if (!isPreparing)
            StartCoroutine("StartupRoutine");
    }

    IEnumerator StartupRoutine()
    {
        isPreparing = true;
        startupSound.Play();
        anim.Play("Startup");
        yield return new WaitForSeconds(anim.GetClip("Startup").length);
        audio.Play();
        isReady = true;
    }

    public void LeftShot()
    {
        mentosCopy = Instantiate(mentos, leftWeapon.position + leftWeapon.forward * 0.75f, Quaternion.identity) as GameObject;
        mentosCopy.rigidbody.AddForce(leftWeapon.forward * force);
        nt.SendMentosPosition(new Vector3[] { mentosCopy.transform.position, leftWeapon.forward * force });
        anim.Blend("Left Shot");
        ao.Play();
    }

    public void RightShot()
    {
        mentosCopy = Instantiate(mentos, rightWeapon.position + rightWeapon.forward * 0.75f, Quaternion.identity) as GameObject;
        mentosCopy.rigidbody.AddForce(rightWeapon.forward * force);
        nt.SendMentosPosition(new Vector3[] { mentosCopy.transform.position, rightWeapon.forward * force });
        anim.Blend("Right Shot");
        ao.Play();
    }

    private float target = 0.3f;
    private float lastCheck = 0;

    float targetX;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (Time.realtimeSinceStartup - lastCheck > target)
        {
            targetX = Camera.main.transform.rotation.eulerAngles.x;
            lastCheck = Time.realtimeSinceStartup;
        }

        leftShoulder.rotation = Quaternion.Lerp(leftShoulder.rotation, Quaternion.Euler(targetX, leftShoulder.transform.rotation.eulerAngles.y, leftShoulder.transform.rotation.eulerAngles.z), Time.deltaTime * 3);
        rightShoulder.rotation = Quaternion.Lerp(leftShoulder.rotation, Quaternion.Euler(targetX, leftShoulder.transform.rotation.eulerAngles.y, leftShoulder.transform.rotation.eulerAngles.z), Time.deltaTime * 3);
    }
}
