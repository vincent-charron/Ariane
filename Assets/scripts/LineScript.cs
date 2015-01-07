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
    private bool nodeActivated;
    private bool upActivation;
    private bool bottomActivation;
    public float timeActivate;
    public float timeNodeActivate;
    public bool active;

	// Use this for initialization
	void Start () {
        distanceToPlayer = 0;
        lineActivated = false;
        nodeActivated = false;
        active = false;

        upActivation = true;
        bottomActivation = true;

        timeActivate = 15;
        timeNodeActivate = 0.1f;
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
            if (active)
            {
                active = false;
                lineActivated = true;
                nodeActivated = true;
            }
        }
        else
        {
			colorLineActivated.a=255;
			colorLineActivated.r=67;
			colorLineActivated.g=120;
            colorLineActivated.b = 210;
            this.GetComponent<LineRenderer>().SetColors(colorLineActivated, colorLineActivated);

            if (nodeActivated && lineActivated)
            {
                timeNodeActivate -= Time.deltaTime;
                if (timeNodeActivate < 0)
                {
                    nodeActivated = false;
                    timeNodeActivate = 0.1f;
                    if (previousNode != null && bottomActivation)
                    {
                        previousNode.GetComponent<LineScript>().ActivateLineBottom(timeActivate);
                    }
                    if (nextNode != null && upActivation)
                    {
                        nextNode.GetComponent<LineScript>().ActivateLineUp(timeActivate);
                    }
                }
            }
            timeActivate -= Time.deltaTime;
            //colorLine.a = 255;
            //this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
            if (timeActivate < 0)
            {
                lineActivated = false;
                upActivation = true;
                bottomActivation = true;
                timeActivate = 15;
                this.GetComponent<LineRenderer>().SetColors(colorLine, colorLine);
            }
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Sphere")
        {
            lineActivated = true;
            nodeActivated = true;
        }
    }

    void ActivateLineUp(float timeLeft)
    {
        lineActivated = true;
        nodeActivated = true;
        bottomActivation = false;
        timeActivate = timeLeft;
    }

    void ActivateLineBottom(float timeLeft)
    {
        lineActivated = true;
        nodeActivated = true;
        upActivation = false;
        timeActivate = timeLeft;
    }
}
