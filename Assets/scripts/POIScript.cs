using UnityEngine;
using System.Collections;

public class POIScript : MonoBehaviour {

    public bool activate;
    private bool activated;
    public GameObject[] poi;

	// Use this for initialization
	void Start () {
        activate = false;
        activated = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (activate && !activated)
        {
            poi = GameObject.FindGameObjectsWithTag("nodeLine");
            foreach (GameObject go in poi)
            {
                if (Vector3.Distance(transform.position, go.transform.position) < 8)
                {
                    go.GetComponent<LineScript>().active = true;
                }
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
