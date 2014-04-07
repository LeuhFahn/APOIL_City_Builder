using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public GameObject camera;
	public LayerMask mask;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Debug.Log (Input.mousePosition);
			Vector3 directionCamera = camera.GetComponent<Camera>().transform.forward;
			RaycastHit hit;
			Debug.DrawRay(camera.transform.position, 100*directionCamera);
			if(Physics.Raycast (camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),out hit, 100, mask))
			{
				Debug.Log (hit.collider.name);
				hit.collider.gameObject.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
	}
}
