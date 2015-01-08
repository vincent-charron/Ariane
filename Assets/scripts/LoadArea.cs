using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class LoadArea : MonoBehaviour {


    private AreaClass container;
    private CreateArea CA;

	// Use this for initialization
    void Start()
    {
        GameObject vrmanager = GameObject.Find("VRManager");
        if (vrmanager)
        {
            vrmanager.GetComponent<VRManagerScript>().ConfigFile = Application.dataPath + "/configuration/Ariane.vrx";
        }
        CA = CreateArea.Instance;
        string[] xmlFiles = Directory.GetFiles(Application.dataPath + "\\xmlFiles\\areasSave", "*.xml");
        if (xmlFiles.Length == 0)
        {
            CA.createArea(new Vector3(0, 0, 0));
        }
        else
        {
            foreach (string file in xmlFiles)
            {
                //désérialisation du fichier XML pour récupérer la zone qui y a été enregistré
                var serializer = new XmlSerializer(typeof(AreaClass));
                var stream = new FileStream(file, FileMode.Open);
                container = serializer.Deserialize(stream) as AreaClass;
                stream.Close();
                //Debug.Log(container.Position);
                CA.createArea(container);
            }

        }
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnApplicationQuit()
    {
        CA.SaveNewArea();
    }
}
