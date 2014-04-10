using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public GameObject camera;
	public LayerMask mask;

	int nNbTour;
	int nNbHexToColor;

	List<GameObject> temp_HexToColor;
	GameObject[] temp_HexToColor_net;

	// Use this for initialization
	void Start () 
	{
		nNbTour = 0;
		temp_HexToColor = new List<GameObject> ();
		temp_HexToColor_net = new GameObject[64];
		nNbHexToColor = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 directionCamera = camera.GetComponent<Camera>().transform.forward;
			RaycastHit hit;
			Debug.DrawRay(camera.transform.position, 100*directionCamera);
			if(Physics.Raycast (camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),out hit, 100, mask))
			{
				Debug.Log (hit.collider.name);
				temp_HexToColor.Add (hit.collider.gameObject);

				temp_HexToColor_net[nNbHexToColor] = hit.collider.gameObject;
				nNbHexToColor++;

				hit.collider.gameObject.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.blue;
			}
		}
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width - 120, Screen.height - 100, 100, 60), "Fin du tour "+nNbTour.ToString()))
		{
			++nNbTour;
			/*
			foreach(GameObject hex in temp_HexToColor)
			{
				hex.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.red;
			}*/
			int i = 0;
			foreach(GameObject hex in temp_HexToColor)
			{
				networkView.RPC("ColorationFromNetwork", RPCMode.All, temp_HexToColor[i]);
				i++;
			}

			temp_HexToColor.Clear();
		}
	}

	// All RPC calls need the @RPC attribute!
	[RPC]
	void ColorationFromNetwork(GameObject HexToColor)
	{
		HexToColor.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.red;
	}
}
