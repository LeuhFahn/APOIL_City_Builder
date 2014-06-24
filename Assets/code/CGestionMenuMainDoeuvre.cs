using UnityEngine;
using System.Collections;

public class CGestionMenuMainDoeuvre : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CloseMenu()
    {
        NGUITools.SetActive(gameObject.GetComponent<Game>().menuMainDoeuvre, false);
        //caseSelected.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.white;

    }
}
