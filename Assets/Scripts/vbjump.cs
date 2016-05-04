using UnityEngine;
using System.Collections;
using Vuforia;

public class vbjump : MonoBehaviour, IVirtualButtonEventHandler{

	public AudioClip shootSound;
	private AudioSource source;
	private float volLowRange=5.5f;
	private float volHighRange=10.0f;

	public GameObject Player;
	public Rigidbody player;
	public GameObject Workspace;
	private float timer =0.0f;
	private bool jumpflag=false;
	void Start(){
		VirtualButtonBehaviour[] vbs = transform.GetComponentsInChildren<VirtualButtonBehaviour> ();
		foreach (VirtualButtonBehaviour item in vbs)
		{
			item.RegisterEventHandler(this);
		}

		Player = GameObject.Find ("Player");
		player = Player.GetComponent<Rigidbody>();
		Workspace=GameObject.Find ("EditorWorkspace");
	}

	public void Update(){
		if (jumpflag==true)
		{	
			if (Player.transform.localPosition.y > 0.2) {
				jumpflag = false;
			}
			if (Player.transform.localPosition.y < 0.2) {
				player.AddRelativeForce (new Vector3 (0f, 2f, 0f));
			}
		}

	}
		

	public void OnButtonPressed (VirtualButtonAbstractBehaviour vb)
	{
		if (Player.transform.localPosition.y < 0.2) {
			jumpflag = true;

			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot (shootSound, vol);

		} else {
			jumpflag = false;
		}
	}

	public void OnButtonReleased (VirtualButtonAbstractBehaviour vb)
	{

		//throw new System.NotImplementedException ();
	}
}
