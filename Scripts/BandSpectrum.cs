using UnityEngine;
using System.Collections;

#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414 
#pragma warning disable 0618

public class BandSpectrum : MonoBehaviour {

	public GameObject[] prefab;

	//private AudioSource audio_pre;

	int ch = 11;
	int lines = 4;

	int cur_Sphere = 0;
	Vector3[] pos = {new Vector3(-6.5f, 11, 0), new Vector3(-2.2f, 11, 0), new Vector3(2.4f, 11, 0), new Vector3(7f, 11, 0)};

	float[] freqData;
	AudioListener listener;
	
	float[] band;
	//GameObject[] g;

	private float nextActionTime = 0.0f;
	private float period = 1.0f;

	void Start(){
		//audio_pre = GetComponent<AudioSource> ();
		//audio_pre.rolloffMode = AudioRolloffMode.Custom;
		//audio_pre.Play ();
		ch = (int)Mathf.Pow (2, ch-1);

		freqData = new float[ch];

		int n = freqData.Length;
		int k = 0;
		for(int j =0;j<freqData.Length;j++)
		{
			n /= 2;
			if(n == 0) break;
			k++;
		}
		band  = new float[k+1];
		//g     = new GameObject[k+1];
		for (int i = 0;i<band.Length;i++)
		{
			band[i] = 0;

			//if(i > 0 && i < lines+1){
				//g[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				//g[i].transform.position = new Vector3(i,0,0);
			//}
		}
		InvokeRepeating("check", 0f, 1.0f/30.0f); // update at 15 fps
		//InvokeRepeating("SpawnSpher", 0f, 1.0f/60.0f); // update at 15 fps
	}
	
	void check()
	{
		//listener.GetSpectrumData(freqData, 0, FFTWindow.Rectangular);
		//AudioListener.GetOutputData (freqData, 1);
		freqData = AudioListener.GetSpectrumData (ch, 0, FFTWindow.Hamming);
		
		int k = 0;
		int crossover = 2;
		for(int i = 0; i< freqData.Length;i++)
		{  
			float d = freqData[i];
			float b = band[k];       
			band[k] = (d>b)? d:b;   // find the max as the peak value in that frequency band.
			if (i > crossover-3)
			{
				k++;
				crossover *= 2;   // frequency crossover point for each band.
				//if(k > 0 && k < lines+1){
				//	g[k].transform.position = new Vector3(g[k].transform.position.x,band[k]*2,0);
				//	Debug.Log(band[12]);
				//}
				band[k] = 0;
			}  
		}
	}

	void Update(){
		if (Time.time > nextActionTime ) {
			nextActionTime = Time.time + period;
			//Debug.Log("TIME = "+nextActionTime);
			//for(int i = 0; i < 4; i++){
			if(band[1] > 0.07f){
				SpawnSphere(0, pos[0]);
			}
			if(band[2] > 0.15f){
				SpawnSphere(1, pos[1]);
			}
			if(band[3] > 0.065f){
				SpawnSphere(2, pos[2]);
			}
			if(band[4] > 0.05f){
				SpawnSphere(3, pos[3]);
			}

			//}
			
			

		}
	}

	void SpawnSphere(int obj_Num, Vector3 posit){
		//if(band[1] > 0.07f)
			Instantiate(prefab[obj_Num], posit, Quaternion.identity);
	}
	//private float sqrt = Mathf().Sqrt;

}
