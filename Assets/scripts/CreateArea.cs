using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public sealed class CreateArea {

    public float sizeArea = 150;
    public float sizePOI = 0.1F;
    public int minPOI = 15;
    public int maxPOI = 20;
    public float distanceBetweenPOI = 20;

    public List<Vector3> existingArea
    {
        get;
        set;
    } //liste des coordonnées des différentes zones de jeux existant déjà
    private List<GameObject> existingAreaObjects;
    private List<GameObject> newAreaObjects;
    private static CreateArea instance = null;
    private static readonly object padlock = new object();
    private int i = 0;

    CreateArea()
    {
        existingArea = new List<Vector3>();
        existingAreaObjects = new List<GameObject>();
        newAreaObjects = new List<GameObject>();
    }

    public static CreateArea Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CreateArea();
                }
                return instance;
            }
        }
    }

    public void createArea(AreaClass area)
    {
        GameObject existingArea = GameObject.CreatePrimitive(PrimitiveType.Cube); //on crée un Cube
        existingArea.transform.position = area.Position;   //le cube se situe à la position précisé
        existingArea.transform.localScale = Vector3.one * sizeArea;    //le cube a une taille donné
        existingArea.transform.parent = GameObject.Find("Area").transform;
        existingArea.name = i.ToString();
        i++;
        existingArea.GetComponent<MeshRenderer>().enabled = false; //le cube est invisible, le mesh renderer est inutile
        existingArea.GetComponent<BoxCollider>().isTrigger = true;   //le cube doit permettre de détecter quand on y entre ou quand on y sort, mais ne doit pas empêcher le passage du joueur
        existingArea.AddComponent<TriggerArea>();    //on ajoute au cube un script permettant de détecter si le joueur part sur une zone non existante
        existingAreaObjects.Add(existingArea);
        createPOI(existingArea, area);
    }

    public void createArea(Vector3 area)
    {
        GameObject newArea = GameObject.CreatePrimitive(PrimitiveType.Cube); //on crée un Cube
        newArea.transform.position = area * sizeArea;   //le cube se situe à la position précisé
        newArea.transform.localScale = Vector3.one * sizeArea;    //le cube a une taille donné
        newArea.transform.parent = GameObject.Find("Area").transform;
        newArea.name = i.ToString();
        i++;
        newArea.GetComponent<MeshRenderer>().enabled = false; //le cube est invisible, le mesh renderer est inutile
        newArea.GetComponent<BoxCollider>().isTrigger = true;   //le cube doit permettre de détecter quand on y entre ou quand on y sort, mais ne doit pas empêcher le passage du joueur
        newArea.AddComponent<TriggerArea>();    //on ajoute au cube un script permettant de détecter si le joueur part sur une zone non existante
        newAreaObjects.Add(newArea);
        createPOI(newArea);
    }

    private void createPOI(GameObject area, AreaClass areaData)
    {
        List<POI> POIList = areaData.Poi;
        int i = 0;
        POIList.ForEach(delegate(POI g)
        {
            GameObject POI = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            POI.transform.parent = area.transform;
            POI.transform.localPosition = g.Position;
            POI.transform.localScale = Vector3.one * sizePOI;
            POI.name = i.ToString();
            POI.tag = "POI";
            i++;
            POI.GetComponent<SphereCollider>().isTrigger = true;
            //POI.GetComponent<SphereCollider>().radius = 0.25f;
            POI.AddComponent<POIScript>();
        });
    }

    private void createPOI(GameObject area)
    {
        bool endCreationPOI = false;
        bool checkDistance = true;
        int nbTry = 0;
        List<GameObject> POIList = new List<GameObject>();
        do
        {
            checkDistance = true;
            Vector3 position = new Vector3(Random.Range(-0.5F, 0.5F), Random.Range(-0.5F, 0.5F), Random.Range(-0.5F, 0.5F));
            POIList.ForEach(delegate(GameObject g)
            {
                if (Vector3.Distance(g.transform.position, position*sizeArea) < distanceBetweenPOI)
                {
                    checkDistance = false;
                }
            });
            if (checkDistance)
            {
                nbTry = 0;
                GameObject POI = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                POI.transform.parent = area.transform;
                POI.transform.localPosition = position;
                POI.transform.localScale = Vector3.one * sizePOI;
                POI.name = POIList.Count.ToString();
                POI.GetComponent<SphereCollider>().isTrigger = true;
                //POI.GetComponent<SphereCollider>().radius = 0.25f;
                POI.AddComponent<POIScript>();
                POI.tag = "POI";
                POIList.Add(POI);
            }
            else
            {
                nbTry++;
            }
            if (POIList.Count == maxPOI || (POIList.Count >= minPOI && POIList.Count < maxPOI && nbTry > 20))
            {
                endCreationPOI = true;
            }

        } while (!endCreationPOI);

    }

    public void SaveNewArea()
    {
        newAreaObjects.ForEach(delegate(GameObject g)
        {
            AreaClass saveArea = new AreaClass();
            saveArea.Position = g.transform.localPosition;
            int i = 0;
            int nbChild = g.transform.childCount;
            for(i=0; i < nbChild; i++)
            {
                POI savePOI = new POI();
                savePOI.Position = g.transform.GetChild(i).localPosition;
                saveArea.Poi.Add(savePOI);
            }
            saveArea.SaveArea();
        });
    }

}