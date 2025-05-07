using UnityEngine;
using System.Collections;

public class BGEffects : MonoBehaviour {
	public float speed_R;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,0,Time.deltaTime*(speed_R/2f)));
	}
}
