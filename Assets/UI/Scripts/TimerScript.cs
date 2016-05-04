using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	public Text counterText;
	public float seconds, minutes;

	// Use this for initialization
	void Start () {
		counterText = GetComponent<Text> () as Text;
	}
	
	// Update is called once per frame
	/*void Update () {
		if (!GameObject.Find ("Main Camera").GetComponent<EasyModeControl> ().exitpressed) {
			Time.timeScale = 1;
			minutes = (int)(Time.time / 60f);
			seconds = (int)(Time.time % 60f);
			counterText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
		} else {
			Time.timeScale = 0;
		}
		if (GameObject.Find ("Main Camera").GetComponent<EasyModeControl> ().exitfalse) {
			Time.timeScale = 1;
		}
	}*/
}
