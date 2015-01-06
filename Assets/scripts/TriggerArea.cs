using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TriggerArea : MonoBehaviour {

    private CreateArea CA;
    private GameObject sphere;

	// Use this for initialization
    void Start()
    {
        CA = CreateArea.Instance;
        sphere = GameObject.Find("Sphere");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Sphere")
        {
            int x, y, z;
            x = (int)Math.Round(sphere.transform.position.x / CA.sizeArea, 0);
            y = (int)Math.Round(sphere.transform.position.y / CA.sizeArea, 0);
            z = (int)Math.Round(sphere.transform.position.z / CA.sizeArea, 0);
            Vector3 pos = new Vector3(x, y, z);

            if (!CA.existingArea.Exists(s => s == pos))
            {
                CA.createArea(pos);
            }

        }
    }

}
