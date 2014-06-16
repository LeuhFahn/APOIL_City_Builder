using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public GameObject camera;
	public LayerMask mask;
	
	public GameObject menuConstruction;

	int nNbTour;
	int nNbHexToColor;

	List<GameObject> temp_HexToColor;
	List<GameObject> Hexagon;

	// Use this for initialization
	void Start () 
	{
		nNbTour = 0;
		temp_HexToColor = new List<GameObject> ();
		Hexagon = new List<GameObject> ();
		nNbHexToColor = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0) && menuConstruction.activeSelf == false)
		{
			Vector3 directionCamera = camera.GetComponent<Camera>().transform.forward;
			RaycastHit hit;
			Debug.DrawRay(camera.transform.position, 100*directionCamera);
			if(Physics.Raycast (camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition),out hit, 100, mask))
			{
				//Debug.Log (hit.collider.name);
                if (!hit.collider.gameObject.GetComponent<hex>().bBlocked)
                {
                    temp_HexToColor.Add(hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<hex>().bBlocked = true;
                    hit.collider.gameObject.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.blue;
                    gameObject.GetComponent<CGestionMenuConstruction>().SetCaseSelected(hit.collider.gameObject);
                    NGUITools.SetActive(menuConstruction, true);
                }
			}
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	void OnGUI()
	{
		/*if (GUI.Button(new Rect(Screen.width - 120, Screen.height - 100, 100, 60), "Fin du tour "+nNbTour.ToString()))
		{

		}*/
	}

	public void FinDeTour()
	{
		++nNbTour;
		
		foreach(GameObject hex in temp_HexToColor)
		{
			networkView.RPC("ColorationFromNetwork", RPCMode.AllBuffered, hex.GetComponent<hex>().nId);
		}
		
		temp_HexToColor.Clear();
	}

	public void AddHexagon(GameObject hex)
	{
		Hexagon.Add (hex);
	}

    public void BlockHex(int nId)
    {
        networkView.RPC("BlockHexFromNetwork", RPCMode.AllBuffered, nId);
    }

	// All RPC calls need the @RPC attribute!
	[RPC]
	void ColorationFromNetwork(int nId)
	{
		Hexagon[nId].transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.red;
	}

    [RPC]
    void BlockHexFromNetwork(int nId)
    {
        Hexagon[nId].GetComponent<hex>().bBlocked = true;
    }
}
