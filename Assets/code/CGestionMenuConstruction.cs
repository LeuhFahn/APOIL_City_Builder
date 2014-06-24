using UnityEngine;
using System.Collections;

public class CGestionMenuConstruction : MonoBehaviour {

	public enum EConstruction
	{
		e_Chapelle,
		e_Collise,
		e_Igloo,
		e_Maison,
		e_Tour
	}

	public GameObject prefabChapelle;
    public GameObject prefabBatiment;
	GameObject caseSelected;

	// Use this for initialization
	void Start () {
		caseSelected = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CloseMenu()
    {
        NGUITools.SetActive(gameObject.GetComponent<Game>().menuConstruction, false);
        caseSelected.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.white;
        //caseSelected = null;
    }

	public void SetCaseSelected(GameObject caseToSelect)
	{
		caseSelected = caseToSelect;
	}

	public void SelectBatimentToConstruct(GameObject SpriteConstruction, int nCoutBois, int nCoutPierre)
	{
        if (gameObject.GetComponent<CGestionRessources>().CanBuild(nCoutBois, nCoutPierre))
        {
            gameObject.GetComponent<CGestionRessources>().AddRemoveRessources(nCoutBois, nCoutPierre);

            InstanciateBatiment(SpriteConstruction.tag, caseSelected.transform.position, new Vector3(1, 0, 0));
            object[] param = { SpriteConstruction.tag, caseSelected.transform.position, new Vector3(0, 1, 0) };
            networkView.RPC("InstanciateBatiment", RPCMode.AllBuffered, SpriteConstruction.tag, caseSelected.transform.position, new Vector3(0, 1, 0));

            CloseMenu();
        }

		
		
	}

    // All RPC calls need the @RPC attribute!
    [RPC]
    public void InstanciateBatiment(string name, Vector3 position, Vector3 vColor)
    {
        GameObject batObj;
        Color color = new Color(vColor.x, vColor.y, vColor.z, 1.0f);
        switch (name)
        {
            case "chapelle":
            {
                batObj = Instantiate(prefabBatiment, position, Quaternion.identity) as GameObject;
                batObj.GetComponent<CBatiment>().InitSite(EConstruction.e_Chapelle);
                break;
            }
            default:
            {
                batObj = Instantiate(prefabBatiment, position, Quaternion.identity) as GameObject;
                batObj.GetComponent<CBatiment>().InitSite(EConstruction.e_Collise);
                break;
            }
        }

        Transform chantier = batObj.transform.FindChild("chantier");

        foreach (Transform child in chantier)
        {
            child.gameObject.renderer.material.color = color;
        }

        gameObject.GetComponent<Game>().AddNewBatiment(batObj);

        gameObject.GetComponent<Game>().BlockHex(caseSelected.GetComponent<hex>().nId);
       
    }
}
