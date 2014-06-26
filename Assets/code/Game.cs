using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	public GameObject camera;
	public LayerMask mask;
	
	public GameObject menuConstruction;
    public GameObject menuMainDoeuvre;
    public GameObject menuIsServer;

	int m_nNbTour;
	int m_nNbHexToColor;
    int m_nNbPlayerHaveFinishTurn;

    bool m_bCanPlay;

	List<GameObject> temp_HexToColor;
	List<GameObject> Hexagon;
    List<GameObject> Batiments; 

    Vector3 m_vInitPosMous;

	// Use this for initialization
	void Start () 
	{
		m_nNbTour = 0;
		temp_HexToColor = new List<GameObject> ();
		Hexagon = new List<GameObject> ();
        Batiments = new List<GameObject>();
		m_nNbHexToColor = 0;
        m_nNbPlayerHaveFinishTurn = 0;
        m_bCanPlay = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (m_bCanPlay)
        {
            if ((Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))  && menuConstruction.activeSelf == false
                                                                              && menuMainDoeuvre.activeSelf == false)
            {
                Vector3 directionCamera = camera.GetComponent<Camera>().transform.forward;
                RaycastHit hit;
                Debug.DrawRay(camera.transform.position, 100 * directionCamera);
                if (Physics.Raycast(camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 100, mask))
                {
                    //Debug.Log (hit.collider.name);
                    if ((Input.GetMouseButtonDown(1) && !hit.collider.gameObject.GetComponent<hex>().bBlocked))
                    {
                        gameObject.GetComponent<CGestionMenuConstruction>().OpenMenu(hit.collider.gameObject);
                    }

                    if ((Input.GetMouseButtonDown(0)))
                    {
                        gameObject.GetComponent<CGestionMenuMainDoeuvre>().OpenMenu(hit.collider.gameObject);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        GestionCamera();

        //gestion des tours
        if (Network.peerType == NetworkPeerType.Server)
        {
            menuIsServer.SetActive(true);
            menuIsServer.transform.FindChild("Label").GetComponent<UILabel>().text = m_nNbPlayerHaveFinishTurn.ToString() + "/" + (Network.connections.Length+1).ToString();

            if (m_nNbPlayerHaveFinishTurn == Network.connections.Length + 1)
            {
                StartNewTurn();
                networkView.RPC("StartNewTurnFromNetwork", RPCMode.AllBuffered);
            }
        }
	}

    void GestionCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            m_vInitPosMous = GetMousePositionInScreen();
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 vDeltaMousePos = GetMousePositionInScreen() - m_vInitPosMous;
            camera.GetComponent<CCamera>().MoveHorizontal(vDeltaMousePos.x);
            camera.GetComponent<CCamera>().MoveVertical(vDeltaMousePos.y);
        }

        camera.GetComponent<CCamera>().ZoomInOut(5.0f * Input.GetAxis("Mouse ScrollWheel"));
    }

    void StartNewTurn()
    {
        ++m_nNbTour;
        m_nNbPlayerHaveFinishTurn = 0;
        /*m_bCanPlay = true;

        foreach (GameObject bat in Batiments)
        {
            bat.GetComponent<CBatiment>().StartNewTurn();
        }*/
    }
    
    Vector3 GetMousePositionInScreen()
    {
        return new Vector3(Input.mousePosition.x / (float)Screen.width, Input.mousePosition.y / (float)Screen.height, 0.0f);
    }

    public void AddNewBatiment(GameObject newBat)
    {
        Batiments.Add(newBat);
    }

	void OnGUI()
	{
		/*if (GUI.Button(new Rect(Screen.width - 120, Screen.height - 100, 100, 60), "Fin du tour "+nNbTour.ToString()))
		{

		}*/
	}

	public void FinDeTour()
	{
        if (m_bCanPlay)
        {
            ++m_nNbTour;

            /*foreach (GameObject hex in temp_HexToColor)
            {
                networkView.RPC("ColorationFromNetwork", RPCMode.AllBuffered, hex.GetComponent<hex>().nId);
            }

            temp_HexToColor.Clear();*/

            if (Network.peerType == NetworkPeerType.Server)
            {
                PlayerFinishTurn();
            }
            else
            {
                networkView.RPC("PlayerFinishTurn", RPCMode.Server);
            }
            m_bCanPlay = false;
        }
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

    [RPC]
    void PlayerFinishTurn()
    {
        ++m_nNbPlayerHaveFinishTurn;
    }

    [RPC]
    void StartNewTurnFromNetwork()
    {
        m_bCanPlay = true;
        foreach (GameObject bat in Batiments)
        {
            bat.GetComponent<CBatiment>().StartNewTurn();
        }
    }
}
