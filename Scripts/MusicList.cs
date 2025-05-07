using UnityEngine;
using System.IO;
using System.Collections;

public class MusicList : MonoBehaviour {

	// Use this for initialization
	public string[] music_list;
	private GameObject mGrid;
	public GameObject music_Button;

	void Start () {
		mGrid = GameObject.FindGameObjectWithTag("Music_Grid");

		//string path = Application.dataPath+"/Resources/Audio/Musics";    //PARA BUILD
		string path = "Assets/Resources/Audio/Musics"; //PARA EDITOR
		//string path = "Assets/Resources/Audio";
		DirectoryInfo dir = new DirectoryInfo (path);
		string ss = "";
		FileInfo[] info = dir.GetFiles ("*.ogg");

		music_list = new string[info.Length];

		for(int i = 0; i < info.Length; i++){
			music_list[i] = Path.GetFileNameWithoutExtension(info[i].Name);
			AddMusicToScroll(music_list[i]);
			if(i == 0){
				GameObject iGlobal = GameObject.FindGameObjectWithTag("GlobalInspect");
				iGlobal.GetComponent<InspectorGlobal>().ChangeMusic(music_list[i]);
			}
		}

		//foreach(FileInfo f in info){
		//	ss += ", "+Path.GetFileNameWithoutExtension(f.Name);
		//}
		//Debug.Log(ss);
	}

	void AddMusicToScroll(string str){
		GameObject smusic = Instantiate (music_Button, mGrid.transform.position, Quaternion.identity) as GameObject;
		smusic.transform.SetParent (mGrid.transform);
		smusic.transform.localScale = new Vector3 (0.9f, 1f, 1f);
		smusic.GetComponent<MusicButton> ().SetMusicName (str);
		Vector2 sGrid = mGrid.GetComponent<RectTransform> ().sizeDelta;
		sGrid = new Vector2 (sGrid.x, sGrid.y+40f);
		mGrid.GetComponent<RectTransform> ().sizeDelta = sGrid;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
