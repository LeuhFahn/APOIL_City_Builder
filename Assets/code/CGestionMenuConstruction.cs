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

	public void SetCaseSelected(GameObject caseToSelect)
	{
		caseSelected = caseToSelect;
	}

	public void MyClickFunction(GameObject SpriteConstruction)
	{

        InstanciateBatiment(SpriteConstruction.tag, caseSelected.transform.position, new Vector3(1,0,0));
        object[] param = { SpriteConstruction.tag, caseSelected.transform.position, new Vector3(0, 1, 0) };
        networkView.RPC("InstanciateBatiment", RPCMode.AllBuffered, SpriteConstruction.tag, caseSelected.transform.position, new Vector3(0, 1, 0));

		Debug.Log (SpriteConstruction.tag);
		NGUITools.SetActive(gameObject.GetComponent<Game>().menuConstruction, false);
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
                batObj = Instantiate(prefabChapelle, position, Quaternion.identity) as GameObject;
                break;
            }
            default:
            {
                batObj = Instantiate(prefabBatiment, position, Quaternion.identity) as GameObject;
                break;
            }
        }

        foreach (Transform child in batObj.transform)
        {
            child.gameObject.renderer.material.color = color;
        }

        
    }
}
