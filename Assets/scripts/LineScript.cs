using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {

    public int distance = 20;
    public GameObject player;
    public int distanceToPlayer;
    public Color colorLine;

	// Use this for initialization
	void Start () {
        distanceToPlayer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        distanceToPlayer = (int)Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer > distance)
        {
            if (distanceToPlayer < (distance + 10))
            {
                colorLine.a = 1-((float)distanceToPlayer/(float)(distance+10));
            }
            else
            {
                colorLine.a = 0;
            }
            this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
        }
        else
        {
            if (colorLine.a == 0)
            {
                colorLine.a = 255;
                this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
            }
        }
	}
}
