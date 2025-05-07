using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InspectorMenu : MonoBehaviour {

	private InspectorGlobal iGlobal;
	private GameObject slider;

	// Use this for initialization
	void Start () {
		iGlobal = GameObject.FindGameObjectWithTag ("GlobalInspect").GetComponent<InspectorGlobal>();
		slider = GameObject.Find ("Slider");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			slider.GetComponent<Slider> ().value = Mathf.InverseLerp(0.7f, 1.5f, 1.0f);
		}
	}

	public void StartGame(){
        SceneManager.LoadScene("game");
		//Application.LoadLevel ("game");
	}

	public void GoScore(){
        SceneManager.LoadScene("highscores");
        //Application.LoadLevel ("highscores");
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void SlideDificult(){

		float value = slider.GetComponent<Slider> ().value;

		GameObject.Find ("Fill_Diff").GetComponent<Image>().color = new Color(value,1f-value,0);

		ColorBlock cb = slider.GetComponent<Slider> ().colors;
		cb.normalColor = new Color(value,1f-value,0);
		cb.pressedColor = new Color(value,1f-value,0);
		cb.highlightedColor = new Color(value,1f-value,0);

		iGlobal.dificult = value;
		iGlobal.ReloadPitch ();

		slider.GetComponent<Slider>().colors = cb;
		GameObject.Find("diff_txt").GetComponent<Text> ().text = value.ToString ("F2");

		
	}
}
