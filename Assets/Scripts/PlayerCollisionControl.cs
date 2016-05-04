using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerCollisionControl : MonoBehaviour {
	private Rigidbody rb;
	private int count;

	public AudioClip shootSound;
	private AudioSource source;
	private float volLowRange=5.5f;
	private float volHighRange=10.0f;

	public static int overalscore;

	public Text scoreText;
	// Use this for initialization
	void Awake(){
		source = GetComponent<AudioSource> ();
	}
	void Start () {
		//overalscore = 0;
		count = 0;
		rb = GetComponent<Rigidbody> ();
		scoreText.text = "Score: " + count.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		//overalscore = count;
		if (!GameObject.Find ("EditorWorkspace").GetComponent<Pathmove> ().playflag) {
			if (overalscore < count)
				overalscore = count;
		}
		//Debug.Log (count);
	}
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("BowlingPins")) {
			other.gameObject.SetActive (false);
			count += 10;

			float vol = Random.Range (volLowRange, volHighRange);
			source.PlayOneShot (shootSound, vol);

			scoreText.text = "Score: " + count.ToString ();
			//scoreText.text = "Score: " + overalscore.ToString ();
		}
	}
		
}
