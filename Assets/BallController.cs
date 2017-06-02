using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;


public class BallController : MonoBehaviour
{
	Vector3 frameBallPositionDifference;
	Vector3 preBallPosition;

	int point = 0;
	Rigidbody rigidbody;
	public Text pointText;
	public Image outimage1;
	public Image outimage2;
	public Image strikeimage1;
	public Image strikeimage2;
	public Image runner1;
	public Image runner2;
	public Image runner3;

	GameObject[] defence;

	public GameObject subCamera;
	Vector3 subCameraDefalutPosition;
	public GameObject tyumokusen;

	/// <summary>
	/// The ball phase. 0:ピッチャー 1,投球中 2,ヒット後
	/// </summary>
	public int ballPhase = 0;
	private int preBallPhase = 0;

	/// <summary>
	/// The pitch style. 0:ストレート, 1:カーブ, 2:シュート
	/// </summary>
	int pitchStyle = 0;

	// Use this for initialization
	void Start ()
	{
		rigidbody = this.gameObject.GetComponent<Rigidbody> ();
		defence = GameObject.FindGameObjectsWithTag ("Defence");

		subCamera = GameObject.Find ("SubCamera");
		subCameraDefalutPosition = subCamera.transform.position;
		subCamera.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey ("down")) {
			rigidbody.velocity = new Vector3 (0, 0, -0.5f);
			ballPhase = 1;
			pitchStyle = Random.Range (0, 3);
		}

		if (pitchStyle == 1) {
			//rigidbody.velocity = new Vector3 (0, 0, -0.5f);
			rigidbody.AddForce (0.2f, 0, 0);
			//ballPhase = 1;
		} else if (pitchStyle == 2) {
			//rigidbody.velocity = new Vector3 (0, 0, -0.5f);
			rigidbody.AddForce (-0.15f, 0, 0);
			//ballPhase = 1;
		}


		//デフェンスを一定距離以内の時、timeScaleを変更
		if (ballPhase == 2) {
			for (int i = 0; i < defence.Length; i++) {
				if (Vector3.Distance (this.transform.position, defence [i].transform.position) < 0.3f) {
					Time.timeScale = 0.08f;
					tyumokusen.SetActive (true);
					break;
				}
				Time.timeScale = 0.5f;
				tyumokusen.SetActive (false);
			}
		}

		if (ballPhase != preBallPhase) {
			if (ballPhase == 2) {
				preBallPosition = this.transform.position;

			} else if (ballPhase == 0) {
				tyumokusen.SetActive (false);

			}
		}
		preBallPhase = ballPhase;


		//カメラが追いかける
		if (ballPhase == 2) {
			subCamera.transform.LookAt (this.transform);
			frameBallPositionDifference = this.transform.position - preBallPosition;

			subCamera.transform.position += new Vector3 (frameBallPositionDifference.x / 4, 0, frameBallPositionDifference.z / 2);
			preBallPosition = this.transform.position;
		}



	}

	public void StartSubCamera(){
		subCamera.SetActive (true);
	}

	public void FinishSubCamera(){
		subCamera.transform.position = subCameraDefalutPosition;
		subCamera.SetActive (false);
	}

	void CameraDefalut ()
	{
		Camera.main.gameObject.transform.position = new Vector3 (0, 0.63f, -0.24f);
		Camera.main.gameObject.transform.rotation = Quaternion.Euler (90, 0, 0);
		Camera.main.gameObject.transform.parent = null;
	}

	void OnCollisionEnter (Collision col)
	{

		if (col.gameObject.tag == "FairZone") {
			Debug.Log ("Fair");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			strikeimage1.enabled = false;
			strikeimage2.enabled = false;
			point += 10;
			pointText.text = point.ToString ();
			SetBallDefault ();
		}
		if (col.gameObject.tag == "onebase") {
			Debug.Log ("onebase");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			strikeimage1.enabled = false;
			strikeimage2.enabled = false;
			runner1.enabled = true;
			//point += 10;
			//pointText.text = point.ToString ();
			SetBallDefault ();
		}
		if (col.gameObject.tag == "twobase") {
			Debug.Log ("twobase");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			strikeimage1.enabled = false;
			strikeimage2.enabled = false;
			runner2.enabled = true;
			//point += 10;
			//pointText.text = point.ToString ();
			SetBallDefault ();
		}

		if (col.gameObject.tag == "homerun") {
			Debug.Log ("homerun");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			strikeimage1.enabled = false;
			strikeimage2.enabled = false;
			point += 1;
			pointText.text = point.ToString ();
			SetBallDefault ();
			SceneManager.LoadScene ("WinScene");

		}
		if (col.gameObject.tag == "FoulZone") {
			Debug.Log ("Foul");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			if (strikeimage1.enabled == true) {
				strikeimage2.enabled = true;
			} else {
				strikeimage1.enabled = true;
			}
			SetBallDefault ();
		}
		if (col.gameObject.tag == "StrikeZone") {
			//Debug.Log ("Strike");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			if (strikeimage1.enabled == false) {
				strikeimage1.enabled = true;
				Debug.Log ("1strike");
	
			} else if (strikeimage1.enabled == true && strikeimage2.enabled == false) {
				strikeimage2.enabled = true;
				Debug.Log ("2strike");

			} else if (strikeimage1.enabled == true && strikeimage2.enabled == true) {
				//outimage1.enabled = true;
				strikeimage1.enabled = false;
				strikeimage2.enabled = false;

				if (outimage1.enabled == true) {
					outimage2.enabled = true;
				} else {
					outimage1.enabled = true;
				}
				Debug.Log ("sanshinn");
			
			} 
			SetBallDefault ();
		}
		if (col.gameObject.tag == "Defence") {
			Debug.Log ("Out");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			strikeimage1.enabled = false;
			strikeimage2.enabled = false;

			if (outimage1.enabled == true) {
				outimage2.enabled = true;
			} else {
				outimage1.enabled = true;
			}

			if (outimage2.enabled == true) {
				SceneManager.LoadScene ("GameOverScene");
			}
			SetBallDefault ();
		}
		if (col.gameObject.tag == "Out") {
			Debug.Log ("Out");
			this.transform.position = new Vector3 (0, 0, 0);
			rigidbody.velocity = new Vector3 (0, 0, 0);
			CameraDefalut ();
			Time.timeScale = 1;
			//Camera.main.orthographicSize = 0.6f;
			strikeimage1.enabled = false;
			strikeimage2.enabled = false;

			if (outimage1.enabled == true) {
				outimage2.enabled = true;
			} else {
				outimage1.enabled = true;
			}
			if (outimage2.enabled == true) {
				SceneManager.LoadScene ("GameOverScene");
			}
			SetBallDefault ();
		}
	}

	public void SetBallDefault ()
	{
		ballPhase = 0;
		pitchStyle = 0;
		rigidbody.velocity = Vector3.zero;
		FinishSubCamera ();
	}
}
	