using UnityEngine;
using System.Collections;

public class hex : MonoBehaviour {

	public int nId;
    public bool bBlocked;

    GameObject m_Batiment;

	// Use this for initialization
	void Start () {
        bBlocked = false;
        m_Batiment = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetBatiment(GameObject bat)
    {
        m_Batiment = bat;
    }
}
