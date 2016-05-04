using UnityEngine;
using System.Collections;

public class MinimapCameraControl : MonoBehaviour {

	public Transform Target;

	void Start()
	{
		//transform.position = new Vector3 (Target.position.x, transform.position.y, Target.position.z);
		//transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
	}

	void LateUpdate()
	{
		transform.position = new Vector3(Target.position.x, Target.position.y+0.1f, transform.position.z+0.2f);
		//transform.position = new Vector3 (Target.position.y, 10.0f, 10.0f);
	}
}
