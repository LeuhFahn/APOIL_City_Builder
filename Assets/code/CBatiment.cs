using UnityEngine;
using System.Collections;

public class CBatiment : MonoBehaviour {

    CGestionMenuConstruction.EConstruction m_type;
    int m_nNbTurnToConstructionEnd;
    int m_nNbLabor;

    public GameObject chantier;
    public GameObject prefabChapelle;
    GameObject batObj;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void InitSite(CGestionMenuConstruction.EConstruction type)
    {
        m_nNbLabor = 0;
        switch (type)
        {
            case CGestionMenuConstruction.EConstruction.e_Chapelle:
            {
                m_nNbTurnToConstructionEnd = 3;
                break;
            }
            default:
            {
                m_nNbTurnToConstructionEnd = 5;
                break;
            }
        }
    }

    public void StartNewTurn()
    {
        if (m_nNbTurnToConstructionEnd > 0)
        {
            --m_nNbTurnToConstructionEnd;
        }
        else if(chantier != null)
        {
            Color col = chantier.transform.FindChild("Cube").gameObject.renderer.material.color;
            Destroy(chantier);
            batObj = Instantiate(prefabChapelle, gameObject.transform.position, Quaternion.identity) as GameObject;
            batObj.transform.parent = gameObject.transform;

            foreach (Transform child in batObj.transform)
            {
                child.gameObject.renderer.material.color = col;
            }
        }
    }
}
