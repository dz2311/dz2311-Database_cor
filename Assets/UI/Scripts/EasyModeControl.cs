using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EasyModeControl : MonoBehaviour {
	public Canvas ExitWindow;

	public Canvas EditorWindow;
	public bool backtogame;
	public Canvas CongratsWindow;
	public bool exitpressed;
	private GUIStyle guistyle=new GUIStyle();
	private Text counterText;
	private float seconds, minutes;
	public GameObject Player;
	public Vector3 StartPosition;

    public float frames = 0;
    public GameObject editorWorkspace;
    private Quaternion StartRotation;

	public void Backtogame(){
		//EditorWindow.enabled = false;
		backtogame = true;
        exitpressed = false;
	}
	public void Congrats(){
		CongratsWindow.enabled = true;
	} 
	public void Exit(){
		ExitWindow.enabled = true;
		exitpressed = true;
	}
	public void Restart(){
		//Application.LoadLevel (Application.loadedLevel);
        frames = 0;
        editorWorkspace.transform.localPosition = new Vector3(0, 0.007f, 0);
        editorWorkspace.GetComponent<Pathmove>().playflag = true;
        Player.transform.localPosition = StartPosition;
        Player.transform.localRotation = StartRotation;
        GameObject pins = GameObject.Find("BowlingPins");
        foreach (Transform p in pins.transform)
        {
            p.gameObject.SetActive(true);
        }
        Player.GetComponent<PlayerCollisionControl>().SetCount(0);

        ExitWindow.enabled = false;
        exitpressed = false;
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

        editorWorkspace = GameObject.Find("EditorWorkspace");

		CongratsWindow = CongratsWindow.GetComponent<Canvas> ();
		CongratsWindow.enabled = false;
		EditorWindow.enabled = true;
		backtogame = false;

		exitpressed = false;
		counterText = GetComponent<Text> () as Text;
		Player = GameObject.Find ("Player");
		StartPosition = Player.transform.localPosition;
        StartRotation = Player.transform.localRotation;

	}
	void OnGUI(){
        frames += Time.deltaTime;
        minutes = Mathf.Floor(frames / 60);//(int)(Time.timeSinceLevelLoad / 60f);
        seconds = Mathf.RoundToInt(frames % 60);//(int)

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
		if (GameObject.Find ("Player").GetComponent<PlayerCollisionControl> ().count == 100)
			Time.timeScale = 0;
	}
}
