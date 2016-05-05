using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class hardModeControl : MonoBehaviour {


		public Canvas ExitWindow;

	public Canvas CongratsWindow;
		public bool exitpressed;
		private GUIStyle guistyle=new GUIStyle();
		private Text counterText;
		private float seconds, minutes;
		public GameObject Player;
		public Vector3 StartPosition;

		public void Exit(){
			ExitWindow.enabled = true;
			exitpressed = true;
		}
	public void Congrats(){
		CongratsWindow.enabled = true;
	} 
		public void Restart(){
			Application.LoadLevel (Application.loadedLevel);
			Player.transform.localPosition = StartPosition;
		}
		public void ExitAnyWay(){
			Application.LoadLevel(0);
		}
		public void Resume(){
			ExitWindow.enabled = false;
			exitpressed = false;
		}
		// Use this for initialization
		void Start () {
			ExitWindow = ExitWindow.GetComponent<Canvas> ();
			ExitWindow.enabled = false;
		CongratsWindow = CongratsWindow.GetComponent<Canvas> ();
		CongratsWindow.enabled = false;
			exitpressed = false;
			counterText = GetComponent<Text> () as Text;
			Player = GameObject.Find ("Player");
			StartPosition = Player.transform.localPosition;

		}
		void OnGUI(){
			minutes = (int)(Time.timeSinceLevelLoad / 60f);
			seconds = (int)(Time.timeSinceLevelLoad % 60f);
			guistyle.fontSize = 50;

			guistyle.normal.textColor = Color.green;
			//guistyle.fontStyle=
			GUI.Label (new Rect (500, 15, 400, 50), minutes.ToString ("00") + ":" + seconds.ToString ("00"),guistyle);
		}
		// Update is called once per frame
		void Update () {

			if (exitpressed)
				Time.timeScale = 0;
			else
				Time.timeScale= 1;
		if (GameObject.Find ("Player").GetComponent<PlayerCollisionControl> ().count == 100)
			Time.timeScale = 0;
		}
	}
