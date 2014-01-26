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
    public ParticleSystem _ps;
    private UpdateMechaRotation _mechaController;

    void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        ns = GameObject.Find("Networking").GetComponent<NetworkingServer>();
        _ps = transform.FindChild("Puskur").particleSystem;
        if (ns == null)
        {
            _mechaController = GameObject.Find("Mecha").GetComponent<UpdateMechaRotation>();
        }
        _ps.enableEmission = false;
        StartCoroutine("TimeOut");
    }

    public void SetStyleAndBadWolf(Style style)
    {
        _style = style;
        transform.FindChild("Moga/middle/middle 1").renderer.sharedMaterial = colors[(int)_style];
        transform.FindChild("Moga/upper/lid").renderer.sharedMaterial = colors[(int)_style];
        if (style == spawner._bigBadWolf)
        {
            transform.FindChild("Moga/middle/eye").renderer.sharedMaterial = _bigBadWolf;
            transform.FindChild("Moga/middle/eye1").renderer.sharedMaterial = _bigBadWolf;
        }
        else
        {
            transform.FindChild("Moga/middle/eye").renderer.sharedMaterial = colors[4];
            transform.FindChild("Moga/middle/eye1").renderer.sharedMaterial = colors[4];
        }
    }

    void OnEnable()
    {
        if (ns == null)
        {
            id = int.Parse(System.DateTime.Now.ToString("hhmmssff"));
            _style = (Style)Random.Range(0, 5);
            transform.FindChild("Moga/middle/middle 1").renderer.sharedMaterial = colors[(int)_style];
            transform.FindChild("Moga/upper/lid").renderer.sharedMaterial = colors[(int)_style];
            StartCoroutine("TimeOut");
            //transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Mentos"))
        {
            if (ns == null)
            {

                print("Collission : " + _style + " : " + spawner._bigBadWolf);
                if (_style == spawner._bigBadWolf) _mechaController.AddScore(250);
                else _mechaController.AddScore(-100);
            }
            spawner.RemoveObject(gameObject);
            _ps.enableEmission = true;
            collider.enabled = false;

        }
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
