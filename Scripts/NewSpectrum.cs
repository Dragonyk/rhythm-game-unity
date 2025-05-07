using UnityEngine;
using System.Collections;

public class NewSpectrum : MonoBehaviour {
	
	public GameObject prefab;

	public GameObject l1;
	public GameObject l2;
	public GameObject r1;
	public GameObject r2;

	public int numOfObjs = 20;
	public float radius = 5f;
	public GameObject[] cubes;
	public float min_spec_L = 0f;
	public float mid_spec_L = 0f;
	public float max_spec_L = 0f;

	public float min_spec_R = 0f;
	public float mid_spec_R = 0f;
	public float max_spec_R = 0f;

	float[] spec_L = new float[1024];
	float[] spec_R = new float[1024];
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < numOfObjs; i++){
			//float angle = i * Mathf.PI * 2 / numOfObjs;
			//Vector3 pos = new Vector3(Mathf.Cos(angle),0, Mathf.Sin(angle))*radius;
			Vector3 pos = new Vector3(12.5f-(i/2f),0,0);
			Instantiate(prefab, pos, Quaternion.identity);
			
		}
		cubes = GameObject.FindGameObjectsWithTag("cubes");
		//Debug.Log (cubes.Length + " EROROROROROROROR");*/
	}
	
	// Update is called once per frame
	void Update () {
		float[] spec_L = AudioListener.GetSpectrumData (1024, 0, FFTWindow.Hamming);
		float[] spec_R = AudioListener.GetSpectrumData (1024, 1, FFTWindow.Hamming);
		//AudioListener.GetOutputData (spec_L, 0);
		//AudioListener.GetOutputData (spec_R, 1);
		mid_spec_L = 0;
		mid_spec_R = 0;

		min_spec_L = 0;
		min_spec_R = 0;

		max_spec_L = 0;
		max_spec_R = 0;

		for (int i = 0; i < spec_L.Length; i ++) {
			mid_spec_L += spec_L[i];
			mid_spec_R += spec_R[i];

			if(i >= 25){
				if(spec_L[i] > min_spec_L)
					min_spec_L = spec_L[i];
				if(spec_R[i] > min_spec_R)
					min_spec_R = spec_R[i];
			}

		//////////////////////////////////////////////////////
			/// 
			if(spec_L[i] > max_spec_L)
				max_spec_L = spec_L[i];
			if(spec_R[i] > max_spec_R)
				max_spec_R = spec_R[i];
		}
		mid_spec_L /= spec_L.Length;
		mid_spec_R /= spec_R.Length;

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



		for (int i = 0; i < numOfObjs; i++) {
			
			
			
			//Transform objVisual = cubes[i].transform.Find("Visual");
			Vector3 prevScale = cubes[i].transform.localScale;
			
			//Transform particle = cubes[i].transform.Find("Particle");
			//Vector3 prevPos = particle.localPosition;
			
			prevScale.y = Mathf.Lerp(prevScale.y, spec_L[i]*40, Time.deltaTime*30);
			//prevPos.y = Mathf.Lerp(prevPos.y, (spectrum[i]+0.01f)*40, Time.deltaTime*30);
			cubes[i].transform.localScale = prevScale;
			//cubes[i].transform.localScale = prevScale;
			//particle.localPosition = prevPos;
		}

	}
}
