using UnityEngine;
using System.Collections;
using System.Timers;

public class LineScript : MonoBehaviour {

    public int distance = 3;
    public GameObject player;
    public GameObject previousNode;
    public GameObject nextNode;
    private int distanceToPlayer;
    public Color colorLine;
    public Color colorLineActivated;
    private bool lineActivated;
    public float timeActivate;

	// Use this for initialization
	void Start () {
        distanceToPlayer = 0;
        lineActivated = false;
        timeActivate = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (!lineActivated)
        {
            distanceToPlayer = (int)Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer > distance)
            {
                if (distanceToPlayer < (distance + 30))
                {
                    colorLine.a = 1 - ((float)distanceToPlayer / (float)(distance + 30));
                }
                else
                {
                    colorLine.a = 0;
                }
                this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
            }
            else
            {
                if (colorLine.a != 255)
                {
                    colorLine.a = 255;
                    this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
                }
            }
        }
        else
        {

            timeActivate -= Time.deltaTime;
			colorLineActivated.a=255;
			colorLineActivated.r=67;
			colorLineActivated.g=120;
			colorLineActivated.b=210;

            this.GetComponent<LineRenderer>().SetColors(colorLineActivated, colorLineActivated);
			//colorLine.a = 255;
			//this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
            if (timeActivate < 0)
            {
                lineActivated = false;
                timeActivate = 3;
                this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
            }
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Sphere")
        {
            lineActivated = true;
            if (nextNode != null)
            {
                nextNode.GetComponent<LineScript>().ActivateLineUp();
            }
            if (previousNode != null)
            {
                previousNode.GetComponent<LineScript>().ActivateLineBottom();
            }
        }
    }

    void ActivateLineUp()
    {
        lineActivated = true;
        if (nextNode != null)
        {
            nextNode.GetComponent<LineScript>().ActivateLineUp();
        }
    }

    void ActivateLineBottom()
    {
        lineActivated = true;
        if (previousNode != null)
        {
            previousNode.GetComponent<LineScript>().ActivateLineBottom();
        }
    }
}
