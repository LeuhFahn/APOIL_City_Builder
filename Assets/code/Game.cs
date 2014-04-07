using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public GameObject camera;
	public LayerMask mask;

	int nNbTour;

	List<GameObject> temp_HexToColor;

	// Use this for initialization
	void Start () 
	{
		nNbTour = 0;
		temp_HexToColor = new List<GameObject> ();
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
				temp_HexToColor.Add (hit.collider.gameObject);
				hit.collider.gameObject.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.blue;
			}
		}
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 70, 100, 60), "Fin du tour "+nNbTour.ToString()))
		{
			++nNbTour;

			foreach(GameObject hex in temp_HexToColor)
			{
				hex.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.red;
			}
			temp_HexToColor.Clear();
		}
	}
}
