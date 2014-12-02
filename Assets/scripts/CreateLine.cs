using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CreateLine : MonoBehaviour {

	public float lineWidth = 1;	//l'épaisseur du fil se traçant derrière la boule
	private int nbSphereLine = 0;	//le nombre de sphère composant le fil actuel
	public Color colorLine = Color.red;	//la couleur du fil
	public Material lineMaterial;	//le matériel du fil
	
	//on enregistre une liste de gameobject qui se relieront entre eux pour former une ligne rouge
	private LineClass lineSave;	//l'objet qui permettra d'enregistrer les différentes informations du fil, afin de les enregistrer et pouvoir recréer le fil à un prochaine partie
	private SphereLine sphereSave;	//objet permettant d'enregistrer les différents informations de la dernière sphère.
	
	private GameObject currentLine;	//le gameObject formant le fil actuel
	private List<GameObject> spheres;	//la liste permettant de stocker l'ensemble des sphères formant le fil.
	private GameObject newSphere; //dernière sphère gameObject du fil
    private List<GameObject> cylinders;
    private GameObject newCylinder;

	// Use this for initialization
	void Start () {
		//création du gameObject fil
		currentLine = new GameObject ("currentLine");
		spheres = new List<GameObject> ();
        cylinders = new List<GameObject>();
        spheres.Add(new GameObject(nbSphereLine.ToString()));
        cylinders.Add(GameObject.CreatePrimitive(PrimitiveType.Capsule));

		//création de l'objet de sauvegarde
		lineSave = new LineClass ("lineObject");
		sphereSave = new SphereLine (nbSphereLine.ToString ());
		lineSave.Spheres.Add(sphereSave);
		sphereSave.Position = this.transform.position;//sauvegarde de la position de la dernière sphère 
		
		//creation du game object correspondant à la dernière sphère
		newSphere = spheres.Last();
		newSphere.transform.parent = currentLine.transform;	//le parent de chaque sphère est le gameObject currentLine
		newSphere.AddComponent<LineRenderer>();
		newSphere.transform.position = this.transform.position;	//la position de la sphère est celle de la boule à l'instant où la sphère est créée
        newSphere.AddComponent<LineScript>();
        newSphere.GetComponent<LineScript>().player = gameObject;
        newSphere.GetComponent<LineScript>().colorLine = colorLine;

        //creation du game object correspondant au dernier cylindre
        /*newCylinder = cylinders.Last();
        newCylinder.name = nbSphereLine.ToString();
        newCylinder.transform.parent = currentLine.transform;
        newCylinder.transform.position = this.transform.position;
        newCylinder.transform.localScale = new Vector3(0.1F, 0.25F, 0.1F);
        newCylinder.collider.enabled = false;*/
	}
	
	// Update is called once per frame
	void Update () {
		//script de création et de sauvegarde du fil actuel
		if (Vector3.Distance(newSphere.transform.position, this.transform.position) > 0.3) {
			nbSphereLine++;
			
			//sauvegarde des paramètres de la dernière sphère
			sphereSave = new SphereLine(nbSphereLine.ToString());
			lineSave.Spheres.Add(sphereSave);
			sphereSave.Width = lineWidth;	//sauvegarde de l'épaisseur du fil
			sphereSave.ColorLine = colorLine;	//sauvegarde de la couleur du fil
			sphereSave.Position = this.transform.position;	//sauvegarde de la position de la sphère
			
			//création du game object correspondant à la dernière sphère
			newSphere = new GameObject (nbSphereLine.ToString());
			LineRenderer lineRenderer = newSphere.AddComponent<LineRenderer>();	//chaque gameObject Sphere a un composant ligne de rendu, permettant de tracer une ligne derrière eux
			newSphere.transform.position = this.transform.position;	//la position de la boule est celle de le sphère à l'instant de sa création
            newSphere.transform.parent = currentLine.transform;	//le gameObject parent de la sphere est le game Object formant la ligne actuelle.
            newSphere.AddComponent<LineScript>();
            newSphere.GetComponent<LineScript>().player = gameObject;
            newSphere.GetComponent<LineScript>().colorLine = colorLine;

            //creation du game object correspondant au dernier cylindre
            /*newCylinder = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            newCylinder.name = nbSphereLine.ToString();
            newCylinder.transform.parent = currentLine.transform;
            newCylinder.transform.position = this.transform.position;
            newCylinder.transform.localScale = new Vector3(0.1F, 0.25F, 0.1F);
            newCylinder.collider.enabled = false;
            newCylinder.transform.up = newCylinder.transform.position - cylinders.Last().transform.position;
            //newCylinder.transform.Translate(-newCylinder.transform.up * Time.deltaTime);
            cylinders.Add(newCylinder);*/

			
			//paramétrage de la ligne de rendu
			lineRenderer.material = lineMaterial;
			lineRenderer.SetWidth(lineWidth, lineWidth);
			lineRenderer.SetColors(colorLine, colorLine);
			lineRenderer.SetPosition(0, spheres.Last().transform.position);
			lineRenderer.SetPosition(1, newSphere.transform.position);
			spheres.Add(newSphere);
		}
	}
	
	void OnApplicationQuit(){
		lineSave.SaveLine ();	//au moment où l'on arrete l'application, on appelle la méthode permettant d'enregistrer toutes les informations du fil qui a été créé dans un fichier XML
	}
}
