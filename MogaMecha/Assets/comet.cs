using UnityEngine;
using System.Collections;

public class comet : MonoBehaviour {

    public Vector3 speed; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(speed);
	}
}
