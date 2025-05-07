using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InspectorSystem : MonoBehaviour {
	public int score_value = 0;
	private Text score_text;
	private InspectorGlobal iGlobal;

	// Use this for initialization
	void Start () {
		iGlobal = GameObject.FindGameObjectWithTag ("GlobalInspect").GetComponent<InspectorGlobal>();
		score_text = GameObject.Find("txt_Score").GetComponent<Text>();
		ChangeScore (0);
	}
	
	// Update is called once per frame
	void Update () {
		ReturnMainMenu ();
		//Debug.Log ("SCORE >>> " + score_value); 
	}

	void ReturnMainMenu(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("main_menu");
		
		}
	}

	public int ChangeScore(int value){
		float newValue = (float)value * Mathf.Lerp(0.7f,1.5f,iGlobal.dificult);
		value = (int)newValue;
		score_value += value;
		if (score_value < 0)
			score_value = 0;
		//Debug.Log ("SCORE >>> " + score_value);
		score_text.text = "Score: "+score_value;
		return value;
	}
}
