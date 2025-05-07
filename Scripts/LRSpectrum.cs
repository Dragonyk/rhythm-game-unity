using UnityEngine;
using System.Collections;

#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414 
#pragma warning disable 0618

public class LRSpectrum : MonoBehaviour {
	
	public GameObject[] prefab;
    //public GameObject[] prefab_christmas;
    private string[] steps = {"Step_A","Step_B","Step_C","Step_D"};
	
	public GameObject l1;
	public GameObject l2;
	public GameObject r1;
	public GameObject r2;

	public float min_spec_L = 0f;
	public float max_spec_L = 0f;
	
	public float min_spec_R = 0f;
	public float max_spec_R = 0f;
	
	float[] spec_L = new float[1024];
	float[] spec_R = new float[1024];

	private float[] valueMin_Small = {0.025f, 0.04f};
	private float[] valueMax_Small = {0.05f, 0.08f};

	private float[] valueMin_Normal = {0.06f, 0.16f};
	private float[] valueMax_Normal = {0.1f, 0.3f};

	private float[] valueMin_Big = {0.22f}; // 0.16
	private float[] valueMax_Big = {0.4f};  // 0.3

	Vector3[] pos;// = {new Vector3(-6.5f, 11, 0), new Vector3(-2.2f, 11, 0), new Vector3(2.4f, 11, 0), new Vector3(7f, 11, 0)};
	
	private float nextActionTime = 0.0f;
	private float period = 1.0f;
	private bool isMic = false;
 	private GameObject iGlobal;
	private GameObject iLocal;

	private bool isScored = false;

	AudioSource audio;
	private AudioClip[] musics;
	
	void Start(){
		audio = Camera.main.gameObject.GetComponent<AudioSource> ();
		iGlobal = GameObject.FindGameObjectWithTag("GlobalInspect");
		iLocal = GameObject.FindGameObjectWithTag ("Inspector");

		if (iGlobal.GetComponent<InspectorGlobal> ().name_music.Equals ("_")) {
			iGlobal.GetComponent<InspectorGlobal> ().ChangeMusic ("_");
			isMic = true;
			audio.loop = true;
		}
		else
			iGlobal.GetComponent<InspectorGlobal>().ChangeMusic("");

		//Debug.Log (musics[0].name);


		float pch = audio.pitch;
		if (pch == 0f) pch = 1f;
		period /= pch;

		//period = 0.25f;

		pos = new Vector3[4];

		for(int i = 0; i < pos.Length; i++){
			GameObject cur_step = GameObject.Find (steps[i]);
			pos [i] = new Vector3 (cur_step.transform.position.x, 11, 0);
		}
	}

	void EndMusic(){
		if (!isScored) {
			int tscore = iLocal.GetComponent<InspectorSystem> ().score_value;
			iGlobal.GetComponent<InspectorGlobal> ().SetScore (tscore);
			iGlobal.GetComponent<InspectorGlobal> ().SaveScores ();
			isScored = true;
		}
		Application.LoadLevel("main_menu");

		//DestroyObject (GameObject.FindGameObjectWithTag("GlobalInspect"));
	}

	IEnumerator WaitTime(float value) {
		yield return new WaitForSeconds(value);

		if(!audio.isPlaying && !isMic){
			EndMusic();
		}
	}
	
	void Update(){
		FrequencyUpdate ();
		SpawnUpdate ();

		if (!audio.isPlaying) {
			//yield return new WaitForSeconds(3);
			StartCoroutine(WaitTime(3f));

		}
		//Debug.Log ("MIN L >>> "+min_spec_L+", MAX L >>> "+max_spec_L+" | MIN R >>> "+min_spec_R+", MAX R >>> "+max_spec_R);
	}

	int DeathSphere(int cur){
		int f = cur;
		int dSph = 4;
		int death_r = 0;

		if (cur < 4) {
			dSph = 4;
			death_r = Random.Range(1,5);
		}
		else{
			dSph = 13;
			death_r = Random.Range(1,7);
		}

		if (death_r == 1)
			f = dSph;


		return f;
	}

	void SpawnUpdate(){
		if (Time.time > nextActionTime ) {
			nextActionTime = Time.time + period;
			//Debug.Log("TIME = "+nextActionTime);


			/////  VERDES /////
			if(min_spec_L >= valueMin_Small[0] && min_spec_L < valueMin_Small[1]){
				SpawnSphere(5, pos[0],0);
			}
			else if(min_spec_L > valueMin_Normal[0] && min_spec_L < valueMin_Normal[1]){
				SpawnSphere(DeathSphere(0), pos[0],0);
			}
			else if(min_spec_L > valueMin_Big[0]){
				SpawnSphere(DeathSphere(9), pos[0],0);
			}

			/////  AMARELAS /////
			if(max_spec_L >= valueMax_Small[0] && max_spec_L < valueMax_Small[1]){
				SpawnSphere(6, pos[1],1);
			}
			else if(max_spec_L > valueMax_Normal[0] && max_spec_L < valueMax_Normal[1]){
				SpawnSphere(DeathSphere(1), pos[1],1);
			}
			else if(max_spec_L > valueMax_Big[0]){
				SpawnSphere(DeathSphere(10), pos[1],1);
			}

			/////  VERMELHAS /////
			if(min_spec_R >= valueMin_Small[0] && min_spec_R < valueMin_Small[1]){
				SpawnSphere(7, pos[2],2);
			}
			else if(min_spec_R > valueMin_Normal[0] && min_spec_R < valueMin_Normal[1]){
				SpawnSphere(DeathSphere(2), pos[2],2);
			}
			else if(min_spec_R > valueMin_Big[0]){
				SpawnSphere(DeathSphere(11), pos[2],2);
			}

			/////  AZUIS /////
			if(max_spec_R >= valueMax_Small[0] && max_spec_R < valueMax_Small[1]){
				SpawnSphere(8, pos[3],3);
			}
			else if(max_spec_R > valueMax_Normal[0] && max_spec_R < valueMax_Normal[1]){
				SpawnSphere(DeathSphere(3), pos[3],3);
			}
			else if(max_spec_R > valueMax_Big[0]){
				SpawnSphere(DeathSphere(12), pos[3],3);
			}
		}
	}

	void FrequencyUpdate(){
		float[] spec_L = AudioListener.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		float[] spec_R = AudioListener.GetSpectrumData (1024, 1, FFTWindow.Hamming);
		
		min_spec_L = 0;
		min_spec_R = 0;
		
		max_spec_L = 0;
		max_spec_R = 0;
		
		for (int i = 0; i < spec_L.Length/2; i ++) {
			
			if(i >= 25){
				if(spec_L[i] > min_spec_L)
					min_spec_L = spec_L[i];
				if(spec_R[i] > min_spec_R)
					min_spec_R = spec_R[i];
			}
			
			///////////////////////////////////////////////////////

			if(spec_L[i] > max_spec_L)
				max_spec_L = spec_L[i];
			if(spec_R[i] > max_spec_R)
				max_spec_R = spec_R[i];
		}
		
		Vector3 l1_scale = l1.transform.localScale;
		Vector3 l2_scale = l2.transform.localScale;
		Vector3 r1_scale = r1.transform.localScale;
		Vector3 r2_scale = r2.transform.localScale;
		
		l1_scale.y = Mathf.Lerp(l1_scale.y, min_spec_L*20, Time.deltaTime*30);
		l2_scale.y = Mathf.Lerp(l2_scale.y, max_spec_L*20, Time.deltaTime*30);
		r1_scale.y = Mathf.Lerp(r1_scale.y, min_spec_R*20, Time.deltaTime*30);
		r2_scale.y = Mathf.Lerp(r2_scale.y, max_spec_R*20, Time.deltaTime*30);

		l1.transform.localScale = l1_scale;
		l2.transform.localScale = l2_scale;
		r1.transform.localScale = r1_scale;
		r2.transform.localScale = r2_scale;
	}
	
	void SpawnSphere(int obj_Num, Vector3 posit, int vstep){
		GameObject sphere = Instantiate(prefab[obj_Num], posit, Quaternion.identity) as GameObject;
		sphere.GetComponent<Sphere_Stats> ().step = vstep;

	}
	
}
