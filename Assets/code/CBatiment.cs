using UnityEngine;
using System.Collections;

public class CBatiment : MonoBehaviour {

    CGestionMenuConstruction.EConstruction m_type;
    int m_nNbTurnToConstructionEnd;
    int m_nNbLabor;

    public GameObject chantier;
    public GameObject prefabChapelle;
    GameObject Game;
    GameObject batObj;

	// Use this for initialization
	void Start () 
    {
        Game = GameObject.Find("_Game");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SetNbLabor(int nNbLabor)
    {
        m_nNbLabor = nNbLabor;
    }

    public int GetNbLabor()
    {
        return m_nNbLabor;
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
            m_nNbTurnToConstructionEnd -= m_nNbLabor;
        }

        if (m_nNbTurnToConstructionEnd < 0 && chantier != null)
        {
            Color col = chantier.transform.FindChild("Cube").gameObject.renderer.material.color;
            Destroy(chantier);
            batObj = Instantiate(prefabChapelle, gameObject.transform.position, Quaternion.identity) as GameObject;
            batObj.transform.parent = gameObject.transform;

            foreach (Transform child in batObj.transform)
            {
                child.gameObject.renderer.material.color = col;
            }

            Game.GetComponent<CGestionRessources>().AddRemoveLaborDispo(m_nNbLabor);
        }
    }
}
