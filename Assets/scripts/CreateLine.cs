﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Vectrosity;

public class CreateLine : MonoBehaviour {

	public float lineWidth = 1;	//l'épaisseur du fil se traçant derrière la boule
    private int nbSphereLine = 0;	//le nombre de sphère composant le fil actuel
    public Color colorLine = Color.red;	//la couleur du fil
    public Color colorLineActivated = Color.blue;	//la couleur que prend le fil si le joueur s'en approche
    public Material lineMaterial;	//le matériel du fil

	//on enregistre une liste de gameobject qui se relieront entre eux pour former une ligne rouge
	private LineClass lineSave;	//l'objet qui permettra d'enregistrer les différentes informations du fil, afin de les enregistrer et pouvoir recréer le fil à un prochaine partie
	private SphereLine sphereSave;	//objet permettant d'enregistrer les différents informations de la dernière sphère.
	
	private GameObject currentLine;	//le gameObject formant le fil actuel
	private List<GameObject> spheres;	//la liste permettant de stocker l'ensemble des sphères formant le fil.
	private GameObject newSphere; //dernière sphère gameObject du fil

	// Use this for initialization
	void Start () {
        audio.Play();
		//création du gameObject fil
		currentLine = new GameObject ("currentLine");
		spheres = new List<GameObject> ();
        spheres.Add(new GameObject(nbSphereLine.ToString()));

		//création de l'objet de sauvegarde
		lineSave = new LineClass ("lineObject");
		sphereSave = new SphereLine (nbSphereLine.ToString ());
		lineSave.Spheres.Add(sphereSave);
		sphereSave.Position = this.transform.position;//sauvegarde de la position de la dernière sphère 
		
		//creation du game object correspondant à la dernière sphère
		newSphere = spheres.Last();
		newSphere.transform.parent = currentLine.transform;	//le parent de chaque sphère est le gameObject currentLine
		newSphere.AddComponent<LineRenderer>();
        SphereCollider sp = newSphere.AddComponent<SphereCollider>();
        sp.isTrigger = true;
		newSphere.transform.position = this.transform.position;	//la position de la sphère est celle de la boule à l'instant où la sphère est créée
        newSphere.AddComponent<LineScript>();
        newSphere.GetComponent<LineScript>().player = gameObject;
        newSphere.GetComponent<LineScript>().previousNode = null;
        newSphere.GetComponent<LineScript>().colorLine = colorLine;
        newSphere.GetComponent<LineScript>().colorLineActivated = colorLineActivated;
    }
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.F))
		{
			Screen.fullScreen = !Screen.fullScreen;
		}

		//script de création et de sauvegarde du fil actuel
		if (Vector3.Distance(newSphere.transform.position, this.transform.position) > 0.5) {
			nbSphereLine++;
			
			//sauvegarde des paramètres de la dernière sphère
			sphereSave = new SphereLine(nbSphereLine.ToString());
			lineSave.Spheres.Add(sphereSave);
            sphereSave.Width = lineWidth;	//sauvegarde de l'épaisseur du fil
            sphereSave.ColorLine = colorLine;	//sauvegarde de la couleur du fil
            sphereSave.ColorLineActivated = colorLineActivated;	//sauvegarde de la couleur du fil
			sphereSave.Position = this.transform.position;	//sauvegarde de la position de la sphère
			
			//création du game object correspondant à la dernière sphère
			newSphere = new GameObject (nbSphereLine.ToString());
			LineRenderer lineRenderer = newSphere.AddComponent<LineRenderer>();	//chaque gameObject Sphere a un composant ligne de rendu, permettant de tracer une ligne derrière eux
			newSphere.transform.position = this.transform.position;	//la position de la boule est celle de le sphère à l'instant de sa création
            newSphere.transform.parent = currentLine.transform;	//le gameObject parent de la sphere est le game Object formant la ligne actuelle.
            SphereCollider sp = newSphere.AddComponent<SphereCollider>();
            sp.isTrigger = true;
            newSphere.AddComponent<LineScript>();
            newSphere.GetComponent<LineScript>().player = gameObject;
            newSphere.GetComponent<LineScript>().colorLine = colorLine;
            newSphere.GetComponent<LineScript>().colorLineActivated = colorLineActivated;
            newSphere.GetComponent<LineScript>().previousNode = spheres.Last();
            spheres.Last().GetComponent<LineScript>().nextNode = newSphere;
            newSphere.GetComponent<LineScript>().nextNode = null;
			
			//paramétrage de la ligne de rendu
			lineRenderer.material = lineMaterial;
			lineRenderer.SetWidth(lineWidth, lineWidth);
			lineRenderer.SetColors(colorLine, colorLine);
			lineRenderer.SetPosition(0, spheres.Last().transform.position);
			lineRenderer.SetPosition(1, newSphere.transform.position);
			spheres.Add(newSphere);
        }
        //vl.Draw3D();
	}

    void OnApplicationQuit()
    {
        if (spheres.Count > 0)
        {
            lineSave.SaveLine();	//au moment où l'on arrete l'application, on appelle la méthode permettant d'enregistrer toutes les informations du fil qui a été créé dans un fichier XML
        }
    }
}
