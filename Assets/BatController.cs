using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatController : MonoBehaviour {

	public float speed;
	Rigidbody rigidbody;
	public GameObject ball;

	bool flag;
	float timer = 1;

	BallController ballController;

	// Use this for initialization
	void Start () {
		//世界の時の流れを支配する
		ballController = ball.GetComponent<BallController>();
		Time.timeScale = 1f;
		rigidbody = this.gameObject.GetComponent<Rigidbody> ();
		rigidbody.centerOfMass = new Vector3 (0, 0, 0);
		//Debug.Log ("hoge");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("space")) {
			flag = true;
			timer = 1;
		}

		if(flag == true){
			timer -= Time.deltaTime;
			if (timer < 0) {
				flag = false;
				this.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			//Debug.Log ("swing");
			rigidbody.angularVelocity = new Vector3 (0, -10, 0);
			//this.transform.Rotate(0.0f, -5.0f, 0.0f);
			//this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
		} else {
			rigidbody.angularVelocity = new Vector3 (0, 0, 0);
		}

	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.tag == "Ball") {
			Debug.Log ("kaki--n");
			//Camera.main.orthographicSize = 3.0f;
			//Camera.main.gameObject.transform.SetParent(ball.transform);
			Camera.main.gameObject.transform.position = new Vector3 (0, 0.19f, -0.83f);
			Camera.main.gameObject.transform.rotation = Quaternion.Euler(8.182f, 0, 0);
			Time.timeScale = 0.5f;
			ballController.ballPhase = 2;
			ballController.StartSubCamera ();
		}

	}
}
