using UnityEngine;
using System.Collections;

public class uberAwesomeMogaMecha : MonoBehaviour
{

    public enum Style
    {
        One,
        Two,
        Three,
        Four,
        Five
    }

    public Style _style;
    public int id = -1;
    Spawner spawner;
    NetworkingServer ns;
    public Material _bigBadWolf;
    public Material[] colors;

    void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        ns = GameObject.Find("Networking").GetComponent<NetworkingServer>();
        StartCoroutine("TimeOut");
    }

    public void SetStyleAndBadWolf(Style style)
    {
        _style = style;
        renderer.sharedMaterial = colors[(int)_style];
        if (style == spawner._bigBadWolf)
        {
            transform.GetChild(0).renderer.sharedMaterial = _bigBadWolf;
        }
        else transform.GetChild(0).renderer.sharedMaterial = null;
    }

    void OnEnable()
    {
        if (ns == null)
        {
            id = int.Parse(System.DateTime.Now.ToString("hhmmssff"));
            _style = (Style)Random.Range(0, 5);
            renderer.sharedMaterial = colors[(int)_style];
            StartCoroutine("TimeOut");
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.name.Equals("Mentos"))
        DestroyTheEnemy();
    }

    IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(15);
        DestroyTheEnemy();
    }

    private void DestroyTheEnemy()
    {
        spawner.RemoveObject(gameObject);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate(-Vector3.forward * 0.15f);
    }
}
