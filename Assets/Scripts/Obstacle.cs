using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public GameObject Workspace;
	public float timer=0;
	// Use this for initialization
	void Start () {
		Workspace=GameObject.Find ("EditorWorkspace");
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer = timer - Time.deltaTime;
			Vector3 move1 = Workspace.transform.localPosition + new Vector3 (-1.5f*timer, 0f, 0f) * Time.deltaTime;
			Workspace.transform.localPosition = move1;
		}
	}


	void OnTriggerEnter(Collider other){
		timer = 1;
		//Vector3 backoff = Workspace.transform.localPosition + new Vector3(-0.5f,0f,0f);
		//Workspace.transform.localPosition = backoff;



	}
}
