using UnityEngine;
using System.Collections;

public class CNotEnough : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnEnable()
    {
        StartCoroutine("Desactivation");
    }

    IEnumerator Desactivation()
    { 
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
