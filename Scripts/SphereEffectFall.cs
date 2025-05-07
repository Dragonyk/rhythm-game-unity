using UnityEngine;
using System.Collections;

#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414

public class SphereEffectFall : MonoBehaviour {

	public GameObject[] prefab;

	private float nextActionTime = 0.0f;
	private float period = 1.0f;
	public AudioClip sndEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextActionTime) {
			nextActionTime = Time.time + period;
			float rand_pos = (float)Random.Range(-18, 18);
			if(rand_pos > -6 && rand_pos < 6){
				if(rand_pos > 0)
					rand_pos = 6;
				else
					rand_pos = -6;
			}
				
			int rand_obj = Random.Range(0, 4);
			int rand_spawn = Random.Range(0, 5);
			//if(rand_spawn == 0)
				SpawnSphere(rand_obj, new Vector3(rand_pos, transform.position.y, 0), 1);
		}
		
	}

	void SpawnSphere(int obj_Num, Vector3 posit, int vstep){
		Instantiate (prefab [obj_Num], posit, Quaternion.identity);
		//sphere.GetComponent<Sphere_Stats> ().step = vstep;
		
	}
}
