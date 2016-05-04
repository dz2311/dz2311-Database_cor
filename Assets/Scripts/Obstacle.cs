using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	public GameObject Workspace;
	// Use this for initialization
	void Start () {
		Workspace=GameObject.Find ("EditorWorkspace");
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnTriggerEnter(Collider other){
		Vector3 backoff = Workspace.transform.localPosition + new Vector3(-0.5f,0f,0f);
		Workspace.transform.localPosition = backoff;
		
	}
}
