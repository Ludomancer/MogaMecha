using UnityEngine;
using System.Collections;

public class UpdateMechaRotation : MonoBehaviour
{


    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
