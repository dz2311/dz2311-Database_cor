using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuControl : MonoBehaviour {

	public Canvas MainMenu;
	public Canvas LevelMenu;
	public Canvas TopScoresMenu;
	public Canvas HelpMenu;
	private static int xxx;
	public Text TopScoreList;

	// Use this for initialization
	void Start () {
		xxx = 0;
		MainMenu = MainMenu.GetComponent<Canvas> ();
		LevelMenu = LevelMenu.GetComponent<Canvas> ();
		TopScoresMenu = TopScoresMenu.GetComponent<Canvas> ();
		HelpMenu = HelpMenu.GetComponent<Canvas> ();
		HelpMenu.enabled = false;
		TopScoresMenu.enabled = false;
		LevelMenu.enabled = false;
		MainMenu.enabled = true;
		TopScoreList.text = "Score: " + xxx.ToString();

	}
	public void HelpMode(){
		HelpMenu.enabled = true;
	}

	public void LevelMode(){
		LevelMenu.enabled = true;

	}
	public void TopScoresMode(){
		TopScoresMenu.enabled = true;
	}
	public void ReturnMode(){
		LevelMenu.enabled = false;
		TopScoresMenu.enabled = false;
		HelpMenu.enabled = false;
	}
	public void PlayMode(){
		Application.LoadLevel (2);
	}
	public void EasyMode(){
		Application.LoadLevel (2);
	}
	public void HardMode(){
		Application.LoadLevel (3);
	}
	public void BuildMode(){
		Application.LoadLevel (1);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
		int trs=PlayerCollisionControl.overalscore;
		//trs = Mathf.Max (trs, xxx);
		//xxx = trs;
		//xxx=0;
		//PlayerPrefs.GetInt("Player Score", )
		TopScoreList.text = "Score: " + trs.ToString();
	}
}
