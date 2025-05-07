using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	public float fadeTime = 3f;

	private bool isfadeIn = true;

	private GameObject fadeObj;
	public float img_a;
	//private Color img_color;

	private float nextActionTime = 0.0f;
	private float period;

	public bool nextFade = false;

	void Start () {
		period = fadeTime / 100f;

		fadeObj = GameObject.Find("fadeUI");
		img_a = fadeObj.GetComponent<Image> ().color.a;

		//StartCoroutine (WaitTime (5f));
	}

	bool FadeIn(){
		bool endFade = false;
		if (Time.time > nextActionTime) {
			nextActionTime = Time.time + period;

			if(img_a >= 0){
				img_a-=0.01f;
			}
			else{ 
				img_a = 0f;
			}

			if(img_a <= 0){
				img_a = 0f;
				isfadeIn = false;
				endFade = true;
			}

			Color iColor = fadeObj.GetComponent<Image> ().color;
			fadeObj.GetComponent<Image> ().color = new Color(iColor.r, iColor.g, iColor.b, img_a);
		}
		return endFade;
	}

	bool FadeOut(){
		bool endFade = false;
		if (Time.time > nextActionTime) {
			nextActionTime = Time.time + period;
			
			if(img_a < 1){
				img_a+=0.01f;
			}
			else{ 
				img_a = 1f;
			}
			
			if(img_a >= 1f){
				img_a = 1f;
				isfadeIn = false;
				endFade = true;
			}
			
			Color iColor = fadeObj.GetComponent<Image> ().color;
			fadeObj.GetComponent<Image> ().color = new Color(iColor.r, iColor.g, iColor.b, img_a);
		}
		return endFade;
	}

	void InOutScreen(){
		bool fadeEnd = false;
		if (img_a > 0 && isfadeIn) {
			fadeEnd = FadeIn();
		}

		if (fadeEnd) {
			StartCoroutine(TimeToFadeOut(3f));
		}

		if (img_a < 1 && !isfadeIn && nextFade) {
			bool tempf = FadeOut();
			if(tempf){
				nextFade = false;
				Application.LoadLevel ("main_menu");
			}
		}

	}



	// Update is called once per frame
	void Update () {
		InOutScreen ();

	
	}

	IEnumerator TimeToFadeOut(float value) {
		yield return new WaitForSeconds(value);
		nextFade = true;
		//Application.LoadLevel ("main_menu");
	}

	IEnumerator WaitTime(float value) {
		yield return new WaitForSeconds(value);
		//Application.LoadLevel ("main_menu");
	}
}
