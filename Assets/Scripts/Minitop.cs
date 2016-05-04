using UnityEngine;
using System.Collections;

public class Minitop : MonoBehaviour {
	public Transform Target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Target.position.x-5f, Target.position.y+30f, transform.position.z);
	}
}
