using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuControl : MonoBehaviour {

	public Canvas MainMenu;
	public Canvas LevelMenu;
	public Canvas TopScoresMenu;
	public Canvas HelpMenu;
	public Canvas InforMenu;
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
		InforMenu.enabled = false;
		TopScoreList.text = "Score: " + xxx.ToString();

	}
	public void HelpMode(){
		HelpMenu.enabled = true;
	}

	public void LevelMode(){
		LevelMenu.enabled = true;

	}

	public void InfoMode(){
		InforMenu.enabled = true;
	}
	public void TopScoresMode(){
		TopScoresMenu.enabled = true;
	}
	public void ReturnMode(){
		LevelMenu.enabled = false;
		TopScoresMenu.enabled = false;
		HelpMenu.enabled = false;
		InforMenu.enabled = false;
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
		int trs_build=PlayerCollisionControl.overalscore_build;
		int trs_easy=PlayerCollisionControl.overalscore_easy;
		int trs_hard=PlayerCollisionControl.overalscore_hard;

		//trs = Mathf.Max (trs, xxx);
		//xxx = trs;
		//xxx=0;
		//PlayerPrefs.GetInt("Player Score", )
		TopScoreList.text = "BuildMode: " + trs_build.ToString()+"\nEasyMode: " + trs_easy.ToString()+"\nHardmode: " + trs_hard.ToString();
		//TopScoreList.text = "Score: " + trs.ToString();
	}
}
