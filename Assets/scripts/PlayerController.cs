using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int speed = 10;	//la vitesse de déplacement de la boule

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//script de déplacement de la boule
		float mouveHorizontal = Input.GetAxis ("Horizontal");
		float mouveVertical = Input.GetAxis ("Vertical");
		Vector3 mouvment = new Vector3 (-mouveVertical, mouveHorizontal, 0);
		//rigidbody.AddForce (mouvment * speed * Time.deltaTime);
		transform.Rotate (mouvment);
		if(Input.GetKey("space")){
			rigidbody.AddForce (transform.forward);
		}
		//transform.eulerAngles = new Vector3 (mouveVertical, mouveHorizontal, 0);

		//Debug.Log (Input.GetKey ("space").ToString ());
		Debug.Log (transform.eulerAngles.x);
	}
}
