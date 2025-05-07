using UnityEngine;
using System.Collections;

#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414 

public class StepScript : MonoBehaviour {


	public int step = 0;
	private KeyCode[] keys = {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F};

	[HideInInspector]
	public bool isHighlight = false;
	[HideInInspector]
	public bool isCount = false;
	public bool isClicked = false;
	private SpriteRenderer render;
	private GameObject[] spheres;
	private Color[] colors = {new Color(0f,1f,0f),new Color(1f,1f,0f),new Color(1f,0f,0f),new Color(0f,0f,1f)};

	Vector2 vecStart;
	Vector2 vecEnd;

	private GameObject inspect;
	private InspectorSystem isys;

	public GameObject float_3DText;

	private float counter = 0.0f;
	// Use this for initialization
	void Start () {


		render = GetComponentInChildren<SpriteRenderer> ();

		vecStart = new Vector2(transform.position.x, transform.position.y-2f);
		//vecEnd = new Vector2(transform.position.x, (transform.position.y+2f));

		//vecStart = transform.position;
		vecEnd = transform.up;
		//vecEnd = new Vector2(transform.position.x+2f, transform.position.y);
		//vecEnd = new Vector2 (0,0);

		inspect = GameObject.FindGameObjectWithTag ("Inspector");
		isys = inspect.GetComponent<InspectorSystem> ();

		//InitFloatText ();
	}

	void CountTime(){
		if (counter < 0.25f) {
			counter += Time.deltaTime * 1f;
		} else {
			isCount = false;
			isHighlight = false;
			render.color = new Color(1f,1f,1f);
			counter = 0.0f;

		}
	}

	void Highlight(){
		float r = 1f;
		float g = 1f;
		float b = 1f;

		if (step == 0)		{r = 0f; g = 1f; b = 0f;}
		else if (step == 1) {r = 1f; g = 1f; b = 0f;}
		else if (step == 2) {r = 1f; g = 0f; b = 0f;}
		else if (step == 3) {r = 0f; g = 0f; b = 1f;}

		render.color = new Color(r,g,b);
	}

	void InitFloatText(string text, Color clr){
		GameObject go_temp = Instantiate (float_3DText, new Vector2(transform.position.x-2f, transform.position.y+3f ), Quaternion.identity) as GameObject;
		go_temp.GetComponentInChildren<Animator>().SetTrigger("isScore");
		TextMesh txt3d = go_temp.GetComponentInChildren<TextMesh> ();
		txt3d.text = text;
		txt3d.color = clr;
		Destroy (go_temp.gameObject, 2f);
	}

	public void AddScore(int value){
		int tvalue = isys.ChangeScore(value);
		string txt = ""+tvalue;
		if (value >= 0)
			txt = "+" + tvalue;
		
		InitFloatText (txt,colors[step]);
	}

	void OnMouseDown(){
		isClicked = true;
	}
	
	// Update is called once per frame
	void Update () {

		//Ray2D ray = new Ray2D(vecStart, vecEnd);
		RaycastHit2D hit = Physics2D.Raycast(vecStart, vecEnd, 3.5f,1 << LayerMask.NameToLayer("Spheres"));
		Debug.DrawLine(vecStart,hit.point, Color.cyan, 3.5f, false);

		if(Input.GetKeyDown(keys[step]) || isClicked){
			if(!isHighlight && !isCount){
				isHighlight = true;
				isCount = true;
			}
			//if(Physics2D.Raycast (vecStart, vecEnd)){
		//Debug.Log("TAG>>>"+hit.collider.gameObject.tag+" | NAME>>>"+hit.collider.gameObject.name);
				if (hit.collider != null) {
					if(hit.collider.gameObject.tag == "Sphere"){
						AddScore(hit.collider.gameObject.GetComponent<Sphere_Stats>().scoreAdd);
						DestroyObject(hit.collider.gameObject);
					}
					//else{
						//isys.ChangeScore(-5);
					//}
				Debug.Log("TAG>>>"+hit.collider.gameObject.tag+" | NAME>>>"+hit.collider.gameObject.name);
				}
				else{
					AddScore(-5);
				}
			isClicked = false;
			//}
		}

		if (isCount) CountTime ();
		if (isHighlight) Highlight ();
	}	
}
