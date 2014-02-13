using UnityEngine;
using System.Collections;

public class AudioObject : MonoBehaviour {

	// Update is called once per frame

    public void Play(AudioClip ac)
    {
        audio.clip = ac;
        audio.Play();
        StartCoroutine("TimeOut");
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(audio.clip.length);
    }
}
