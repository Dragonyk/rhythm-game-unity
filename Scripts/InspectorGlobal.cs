using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class InspectorGlobal : MonoBehaviour {

	//[HideInInspector]
	public string name_music;
	public float dificult;

	[HideInInspector]
	public AudioClip cur_music;

	public int[] scores;

	//public ArrayList<Int32> scores; https://l.facebook.com/l.php?u=https%3A%2F%2Fnhentai.net%2Fg%2F170061%2F&h=ATOnowB1B7Rtj23Oj0QrSA5P0kw0qB8JMVaIEPArG0w-_FVXSNsySDd0O3V1nHXWPmKbL6RZUm_tBmG9yNI_uvbT9GU3phUx-NKC9cnK2_Wi8HQsZb8ym3qjITVqop_Xu8gzmbAAPV1j1XjvAcM
	///https://l.facebook.com/l.php?u=https%3A%2F%2Fwww.baixarhentai.net%2Fhentai%2Fhonoo-no-haramase-tenkousei&h=ATPzdBmYlx8lAad-FTpnx7Jj-NKM4vkZStlLf-9S4XuUQE6qFBJA-dy-DDKqW6ZnY3uYGu1syr3xeZl95LLhUGvkr4QnqCtHsUsJ5MJRVUPDMQqrpX7brvi32qL82qW86lhRLrPc_YkdSRPJUb0
	//public ArrayList  /// http://l.facebook.com/l.php?u=http%3A%2F%2Fhentaik.net%2Fsegredos-de-irmaos-hq-comics%2F&h=ATM5ffeCiAM4FfCt_am_4b2E4rZCImQOhGwU5Z-W-TbNkKlOEkm7c9IbcX1YWlPEFKEH59TMLGauTI0IXBTQ6pRTTq9bl3jsfvWroC5g4m9bOrBYx4BvMnw93YmmAgb-9RzyWCZmet_9CPBpgAWkCy8w1MO7Bzg&enc=AZNU3t6NJFvCmqmgjFILUxNYECUSXzQknQnl4ygH_cCiKiOAMIy8NR-M86XwDL2qbQexBA309h2kW78keu9syw7npTEAmk7fuJthbz6oHt2wmuQQ_Z86ZSjhhROgemY4LJZWctu4159koBjY5BtTEFTxWNWWl65rZDEoHTyS9s9fU2j9hBGKT9JDlE-NToumRh2bgObfJa8inSBcZ_accxS0
	//https://l.facebook.com/l.php?u=https%3A%2F%2Fleitura.baixarhentai.net%2Fread%2Fsarashinake_no_ketsumyaku__the_sarashina_bloodline_comecome_selection%2Fpt-br%2F0%2F1%2Fpage%2F1&h=ATM9XpOKsdixiRO9zYdnm6PwrMe9c6tF9rm20gkgfalh-I2JUB4nHklDdm4PBMw2oonEr4hXPJLmcbSCNB7MN7V3eC_-Y3let2dFy-0BzH1FVt_te1pWWD1Ua3J6r9M44M18yBoPOBiXz0BMf1o
	// Use this for initialization 
	void Start () {
		scores = new int[10];
		ReadScore ();
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	IEnumerator LoadMusic (string nMusic){
		if (!nMusic.Equals ("")) {
			name_music = nMusic;
			//Debug.Log("Deu CERTO UE" + name_music); 
		}

		if (nMusic.Equals ("_")) {
			cur_music = Microphone.Start ("Built-in Microphone", true, 1, 44100);
		//	aud.Play ();
		} else {

			WWW www = new WWW ("file://" + Application.dataPath + "/Resources/Audio/Musics/" + name_music + ".ogg");
		
			yield return www;

			//AudioClip aclip = null;
			cur_music = null;
		
			if (www.GetAudioClip() != null) {
				cur_music = www.GetAudioClip();
			} else {
				Debug.Log ("Music not found");
			}
		}

		//cur_music = (AudioClip)Resources.Load (path) as AudioClip;
		//cur_music = aclip;
		AudioSource aSource = Camera.main.GetComponent<AudioSource> ();
		aSource.clip = cur_music;
		aSource.pitch = Mathf.Lerp (0.7f,1.5f,dificult);
		aSource.Play ();
	}

	public void ReloadPitch(){
		AudioSource aSource = Camera.main.GetComponent<AudioSource> ();
		aSource.pitch = Mathf.Lerp (0.7f,1.5f,dificult);
	}

	public void ChangeMusic(string nMusic){
		StartCoroutine (LoadMusic(nMusic));
	}

	public void SetScore(int score){
		if (score > scores [scores.Length - 1]) {
			scores [scores.Length - 1] = score;
		}
		Array.Sort (scores);
		Array.Reverse (scores);
	}

	// POS 0 ... 9
	public int GetScore(int pos){
		int score_temp = -1;
		if (pos >= 0 && pos < 10) {
			score_temp = PlayerPrefs.GetInt("score_"+pos);
		}
		return score_temp;
	}

	public void ReadScore(){
		int[] temp_scores = new int[10];

		for (int i = 0; i < temp_scores.Length; i++) {
			temp_scores[i] = PlayerPrefs.GetInt("score_"+i);
		}

		scores = temp_scores;
		Array.Sort (scores);
		Array.Reverse (scores);
	}

	public void ClearScore(){
		if (Application.loadedLevelName == "main_menu") {
			for (int i = 0; i < 10; i++) {
				PlayerPrefs.SetInt ("score_" + i, 0);
			}
			ReadScore ();
		}
	}

	public void SaveScores(){
		Array.Sort (scores);
		Array.Reverse (scores);


		for (int i = 0; i < scores.Length; i++) {
			PlayerPrefs.SetInt("score_"+i, scores[i]);
		}


		//PlayerPrefs.SetInt ("score0",score); 
	}

	/*public void ChangeMusic(string nMusic){
		if (!nMusic.Equals ("")) {
			name_music = nMusic;
		} 

		string path = "Audio/Musics/"+name_music;    //PARA BUILD
		//string path = "Audio/Musics/"+name_music;  //PARA EDITOR

		//string path = "Audio/"+name_music;
		cur_music = (AudioClip)Resources.Load (path) as AudioClip;
		AudioSource aSource = Camera.main.GetComponent<AudioSource> ();
		aSource.clip = cur_music;
		aSource.Play ();

	}*/
	
	// Update is called once per frame   
	void Update () {
		if (Input.GetKeyDown ("0")) {
			ClearScore();
		}
	}
}
