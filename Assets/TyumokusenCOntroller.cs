using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyumokusenCOntroller : MonoBehaviour {


	void Start(){
		this.gameObject.GetComponent<RectTransform>().Rotate(new Vector3(0,0,Random.Range(-360,360)));

	}

	void Update () {
		Debug.Log ("tyumokusen");
		this.gameObject.GetComponent<RectTransform>().Rotate(new Vector3(0,0,Random.Range(-360,360)));
	}
}
