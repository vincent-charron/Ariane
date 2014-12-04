using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Line")]
public class LineClass{

	[XmlArray("Spheres")]
	[XmlArrayItem("sphere")]
	public List<SphereLine> Spheres {
				get;
				set;
	}

	[XmlAttribute("name")]
	public string Name {
				get;
				set;
		}

	public LineClass(){
		Spheres = new List<SphereLine> ();
		Name = "";
	}

	public LineClass(string name){
		Spheres = new List<SphereLine> ();
		Name = name;
	}

	public void SaveLine(){
		string nameFile = Path.GetRandomFileName ().Replace(".", "X")+Path.GetRandomFileName ().Replace(".", "2")+".xml";
		var serializer = new XmlSerializer(typeof (LineClass));
		var stream = new FileStream(Application.dataPath+"\\xmlFiles\\"+nameFile, FileMode.Create);
		serializer.Serialize(stream, this);
		stream.Close();
	}
}

public class SphereLine{

	public string Name {
				get;
				set;
		}

	public Vector3 Position {
				get;
				set;
	}

	public float Width {
				get;
				set;
	}

    public Color ColorLine
    {
        get;
        set;
    }

    public Color ColorLineActivated
    {
        get;
        set;
    }

	public SphereLine(){
		Name = "";
		Position = new Vector3 (0, 0, 0);
		Width = 0;
		ColorLine = Color.white;
        ColorLineActivated = Color.black;
	}

	public SphereLine(string name){
		Name = name;
		Position = new Vector3 (0, 0, 0);
		Width = 0;
        ColorLine = Color.white;
        ColorLineActivated = Color.black;
	}
}
