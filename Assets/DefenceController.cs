using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefenceController : MonoBehaviour {


	float timer =0;

	// Use this for initialization
	void Start () {
		//バッターボックスを向き
		this.transform.LookAt(new Vector3(0,0,-0.55f));

		//左右ランダムに横を向く
		int lookDirection = Random.Range(0,2);
		if (lookDirection == 0) {
			this.transform.Rotate (new Vector3 (0, 90, 0));
		} else {
			this.transform.Rotate (new Vector3 (0, -90, 0));
		}

		//timerの初期値をランダムいする
		timer = Random.Range(-1.0f,1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		this.transform.position += this.transform.forward * 0.1f * Time.deltaTime;
		if(timer > 1){
			transform.Rotate(new Vector3(0,180,0));
			timer = 0;
		}
			
	}
}
