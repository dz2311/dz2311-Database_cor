using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerCollisionControl : MonoBehaviour {
	private Rigidbody rb;
	private int count;
	public GameObject Cam;
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

    public void SetCount(int c)
    {
        count = c;
        scoreText.text = "Score: " + count.ToString();
    }

	void Start () {
		//overalscore = 0;
		count = 0;
		rb = GetComponent<Rigidbody> ();
		scoreText.text = "Score: " + count.ToString ();
		Cam= GameObject.Find("ARCamera");
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
			if (count == 100) {
				int x;
				x = Application.loadedLevel;
				if (x == 1) {
					EasyModeControl link = Cam.GetComponent<EasyModeControl> ();
					link.Congrats ();
				} else if (x == 2) {
					realEasyModeControl link_ = Cam.GetComponent<realEasyModeControl> ();
					link_.Congrats ();
				} else if (x == 3) {
					hardModeControl link__ = Cam.GetComponent<hardModeControl> ();
					link__.Congrats ();
				}
			}
			//scoreText.text = "Score: " + overalscore.ToString ();
		}
	}
		
}
