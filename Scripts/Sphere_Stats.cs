using UnityEngine;
using System.Collections;

#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414 

public class Sphere_Stats : MonoBehaviour {
	public float speed = 5f;
	public int scoreAdd = 5;
	[HideInInspector]
	public bool inSquare = false;
	public int step = 0;
	private string[] steps = {"Step_A","Step_B","Step_C","Step_D"};
	private KeyCode[] keys = {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F};

	private GameObject cur_step;
	private StepScript ss; //SCRIPT DOS STEPS

	private GameObject inspect;
	private InspectorSystem isys;
	private bool isEffect = false;

	private AudioClip sndEffect;
	private AudioSource audioS;

	// Use this for initialization
	void Start () {

		if (Application.loadedLevelName == "game") {
			if(Camera.main.gameObject.GetComponent<AudioSource> ()!=null)
				speed *= Camera.main.gameObject.GetComponent<AudioSource> ().pitch;

			cur_step = GameObject.Find (steps [step]);
			ss = cur_step.GetComponent<StepScript> ();

			inspect = GameObject.FindGameObjectWithTag ("Inspector");
			isys = inspect.GetComponent<InspectorSystem> ();
		} else {
			GameObject spawnEff =  GameObject.Find("SpawnEffect");
			sndEffect = spawnEff.GetComponent<SphereEffectFall>().sndEffect;
			audioS = spawnEff.GetComponent<AudioSource>();
			isEffect = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0,Time.deltaTime*-speed,0);
		/*if (Input.GetKeyDown (keys[step])) {
			//if (!ss.isHighlight && !ss.isCount) {
			//	ss.isHighlight = true;
			//	ss.isCount = true;
			//}
			if (inSquare) {
				isys.ChangeScore(scoreAdd);
				//DestroyObject(gameObject);
			}
			else{
				isys.ChangeScore(-5);
			}
		}*/

	}

	void OnMouseDown(){
		if (inSquare) {
			if (!ss.isHighlight && !ss.isCount) {
				ss.isHighlight = true;
				ss.isCount = true;
			}
			ss.AddScore(scoreAdd);
			DestroyObject (gameObject);
		}
		if(isEffect){
			float ptc = (float)Random.Range(8,21)/10f;
			audioS.pitch = ptc;
			audioS.PlayOneShot(sndEffect);

			DestroyObject (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.name == steps[step]){
			inSquare = true;
			//DestroyObject(gameObject);
		}
		if(col.gameObject.tag == "End_Sphere"){
			DestroyObject(gameObject);
		}

	}

	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.name == steps[step]){
			inSquare = false;
			//DestroyObject(gameObject);
		}
	}
}
