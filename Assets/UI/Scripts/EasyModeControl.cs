using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EasyModeControl : MonoBehaviour {
	public Canvas ExitWindow;

	public Canvas EditorWindow;
	public bool backtogame;

	public bool exitpressed;
	private GUIStyle guistyle=new GUIStyle();
	private Text counterText;
	private float seconds, minutes;
	public GameObject Player;
	public Vector3 StartPosition;

	public void Backtogame(){
		EditorWindow.enabled = false;
		backtogame = true;
	}

	public void Exit(){
		ExitWindow.enabled = true;
		exitpressed = true;
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

		EditorWindow.enabled = true;
		backtogame = false;

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
		bool flag;
		flag = GameObject.Find ("EditorWorkspace").GetComponent<Pathmove> ().playflag;
		if (exitpressed)
			Time.timeScale = 0;
		else if (!flag)
			Time.timeScale = 0;
		else if (!backtogame)
			Time.timeScale = 0;
		else
			Time.timeScale= 1;
	}
}
