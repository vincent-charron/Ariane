using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class POIScript : MonoBehaviour {

    public bool activate;
	private bool activated;
	public GameObject[] poi;
	public List<GameObject> poiInside;
    private AudioSource AS;
	
	// Use this for initialization
	void Start () {
        activate = false;
		activated = false;
        AS = gameObject.GetComponent<AudioSource>();
        AS.clip = Resources.Load("static_poi") as AudioClip;
        AS.maxDistance = 25;
        AS.minDistance = 10;
        AS.rolloffMode = AudioRolloffMode.Linear;
        AS.loop = true;
        AS.Play();
		poi = GameObject.FindGameObjectsWithTag("nodeLine");
		poiInside = new List<GameObject> ();
		foreach (GameObject go in poi)
		{
			if (Vector3.Distance(transform.position, go.transform.position) < 8)
			{
				poiInside.Add(go);
				go.layer = LayerMask.NameToLayer("POI");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (activate && !activated)
        {
			foreach (GameObject go in poiInside)
			{
    			go.GetComponent<LineScript>().active = true;
				go.layer = LayerMask.NameToLayer("Default");
                AS.Stop();
            }
            activated = true;
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Sphere")
        {
            activate = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
