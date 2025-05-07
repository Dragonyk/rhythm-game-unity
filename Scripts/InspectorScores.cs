using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class InspectorScores : MonoBehaviour {

	public GameObject[] obj_Scores;
	private InspectorGlobal iGlobal;
	// Use this for initialization 
	void Start () {
		//obj_Scores = GameObject.FindGameObjectsWithTag("score_txt"); 
		iGlobal = GameObject.FindGameObjectWithTag("GlobalInspect").GetComponent<InspectorGlobal>();

		SetScores ();
	}

	void SetScores(){
		int[] scores = iGlobal.scores;
		Array.Sort (scores);
		Array.Reverse (scores);

		for(int i = 0; i < obj_Scores.Length; i++){
			obj_Scores[i].GetComponent<Text>().text = scores[i].ToString();
		}
		//Debug.Log ();
	}
	
	// Update is called once per frame ///
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("main_menu");
		}
	}
}
