using UnityEngine;
using System.Collections;

public class Drawline : MonoBehaviour {
	public Transform origin;
	public GameObject destination_parent;

	private Transform destination;

	private LineRenderer lineRenderer; 
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetPosition (0, origin.position);
		lineRenderer.SetWidth (0.01f, 0.01f);
	}

	// Update is called once per frame
	void Update () {
		destination = null;
		Vector3 pointA = origin.position;
		float dist = float.MaxValue;
		foreach (Transform child in destination_parent.transform) {
			float rt_dist = Vector3.Distance (pointA, child.position);
			if (dist > rt_dist && child.gameObject.active && child.position.x > pointA.x) {
				destination = child;
				dist = rt_dist;
			}
		}
		Vector3 pointB = destination.position;
		lineRenderer.SetPosition (0, origin.position);
		//Vector3 pointline = pointA - pointB;
		lineRenderer.SetPosition (1, destination.position);
		if (destination == null)
			lineRenderer.SetWidth (0.0f, 0.0f);
	}
}
