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
		GameObject batObj = Instantiate(prefabBatiment, caseSelected.transform.position, Quaternion.identity) as GameObject;
		Debug.Log (SpriteConstruction.tag);
		NGUITools.SetActive(gameObject.GetComponent<Game>().menuConstruction, false);
	}
}
