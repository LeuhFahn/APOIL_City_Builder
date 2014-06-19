using UnityEngine;
using System.Collections;

public class CMenuConstructionItem : MonoBehaviour {

    public GameObject m_Game;
    public CGestionMenuConstruction.EConstruction m_type;

    int m_nCoutBois;
    int m_nCoutPierre;

	// Use this for initialization
	void Start () 
    {
        switch (m_type)
        {
            case CGestionMenuConstruction.EConstruction.e_Chapelle:
            {
                m_nCoutBois = 10;
                m_nCoutBois = 10;
                break;
            }
            default:
            {
                m_nCoutBois = 0;
                m_nCoutBois = 0;
                break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnClick()
    {
        m_Game.GetComponent<CGestionMenuConstruction>().SelectBatimentToConstruct(gameObject, m_nCoutBois, m_nCoutPierre);
    }
}
