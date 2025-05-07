using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicButton : MonoBehaviour {


	public string music_Name;
	public bool micOn = false;
	private MusicButton micButton;
	// Use this for initialization
	void Start () {
		micButton = GameObject.FindGameObjectWithTag ("Mic").GetComponent<MusicButton> ();
	}

	public void SetMusicName(string name){
		music_Name = name;
		GetComponentInChildren<Text>().text = music_Name;
	}

	public void SetCurrentMusic(){
		string temp_music = music_Name;
		if (micOn && music_Name.Equals ("_")) {
			temp_music = GameObject.Find ("Inspector_mmenu").GetComponent<MusicList> ().music_list [0];
			micOn = false;
		} else if (!micOn && music_Name.Equals ("_")) {
			micOn = true;
		}

		if (!music_Name.Equals ("_")) {
			if (!micButton.micOn) {
				GameObject iGlobal = GameObject.FindGameObjectWithTag ("GlobalInspect");
				iGlobal.GetComponent<InspectorGlobal> ().ChangeMusic (temp_music);
			}
		} else {
			GameObject iGlobal = GameObject.FindGameObjectWithTag("GlobalInspect");
			iGlobal.GetComponent<InspectorGlobal>().ChangeMusic(temp_music);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
