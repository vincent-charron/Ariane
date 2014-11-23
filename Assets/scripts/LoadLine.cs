using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class LoadLine : MonoBehaviour {
	
	public Material lineMaterial;
	public Color lineColor = Color.clear;
	public GameObject previousLines;

	//liste contenant l'ensemble des sphères formant la ligne
	private List<GameObject> spheres;
	private GameObject newSphere; //dernière sphère gameobject créée

	private LineClass container;

	// Use this for initialization
	void Start () {
		int i = 0;
		int j = 0;
		GameObject line;
		previousLines = new GameObject ("previousLines");

		string[] xmlFiles = Directory.GetFiles (Application.dataPath+"\\xmlFiles", "*.xml");

		foreach (string file in xmlFiles) {
			i = 0;
			line = new GameObject(j.ToString());
			j++;
			//désérialisation du fichier XML pour récupérer le fil qui y a été enregistré
			var serializer = new XmlSerializer (typeof(LineClass));
			var stream = new FileStream (file, FileMode.Open);
			container = serializer.Deserialize (stream) as LineClass;
			stream.Close ();

			//recréation du fil dans le jeu
			spheres = new List<GameObject> ();
			newSphere = new GameObject (i.ToString ());
			newSphere.transform.position = container.Spheres [0].Position;
			newSphere.transform.parent = line.transform;
			spheres.Add (newSphere);
			for (i=1; i< container.Spheres.Count; i++) {
				newSphere = new GameObject (i.ToString ());
				LineRenderer lineRenderer = newSphere.AddComponent<LineRenderer> ();
				newSphere.transform.position = container.Spheres [i].Position;
				lineRenderer.material = lineMaterial;
				lineRenderer.SetWidth (container.Spheres [i].Width, container.Spheres [i].Width);
				if(lineColor != Color.clear){
					lineRenderer.SetColors (lineColor, lineColor);
				}else{
					lineRenderer.SetColors (container.Spheres [i].ColorLine, container.Spheres [i].ColorLine);
				}
				lineRenderer.SetPosition (0, spheres.Last ().transform.position);
				lineRenderer.SetPosition (1, newSphere.transform.position);
				newSphere.transform.parent = line.transform;
				spheres.Add (newSphere);
			}
			line.transform.parent = previousLines.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
