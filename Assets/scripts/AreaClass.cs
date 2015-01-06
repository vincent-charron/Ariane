using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Area")]
public class AreaClass
{

    [XmlArray("POIs")]
    [XmlArrayItem("POI")]
    public List<POI> Poi
    {
        get;
        set;
    }

    [XmlElement("position")]
    public Vector3 Position
    {
        get;
        set;
    }

    public AreaClass()
    {
        Poi = new List<POI>();
        Position = new Vector3(0, 0, 0);
    }

    public AreaClass(Vector3 position)
    {
        Poi = new List<POI>();
        Position = position;
    }

    public void SaveArea()
    {
        string nameFile = Path.GetRandomFileName().Replace(".", "A") + Path.GetRandomFileName().Replace(".", "1") + ".xml";
        var serializer = new XmlSerializer(typeof(AreaClass));
        var stream = new FileStream(Application.dataPath + "\\xmlFiles\\areasSave\\" + nameFile, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
}

public class POI
{
    public Vector3 Position
    {
        get;
        set;
    }

    public bool POIActivated
    {
        get;
        set;
    }

    public POI()
    {
        Position = new Vector3(0, 0, 0);
        POIActivated = false;
    }

    public POI(bool activate)
    {
        Position = new Vector3(0, 0, 0);
        POIActivated = activate;
    }
}
