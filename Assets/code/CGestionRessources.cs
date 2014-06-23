using UnityEngine;
using System.Collections;

public class CGestionRessources : MonoBehaviour {

    int m_nRessourcesBois;
    int m_nRessourcesPierre;

    public UILabel textRessourcesBois;
    public UILabel textRessourcesPierre;
    public UILabel textRessourcesNotEnough;

	// Use this for initialization
	void Start () 
    {
        m_nRessourcesBois = 100;
        m_nRessourcesPierre = 20;
        SetUIText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CanBuild(int nCoutBois, int nCoutPierre)
    {
        if (nCoutBois > m_nRessourcesBois && nCoutPierre > m_nRessourcesPierre)
        {
            textRessourcesNotEnough.text = "Not enough resources dude!";
            textRessourcesNotEnough.gameObject.SetActive(true);
        }
        else
        {
            if (nCoutBois > m_nRessourcesBois)
            {
                textRessourcesNotEnough.text = "Not enough wood dude!";
                textRessourcesNotEnough.gameObject.SetActive(true);
            }
			else if (nCoutPierre > m_nRessourcesPierre)
            {
                textRessourcesNotEnough.text = "Not enough stone dude!";
                textRessourcesNotEnough.gameObject.SetActive(true);
            }
        }
		return (nCoutBois <= m_nRessourcesBois && nCoutPierre <= m_nRessourcesPierre);
    }

    public void AddRemoveRessources(int nCoutBois, int nCoutPierre)
    {
        m_nRessourcesBois -= nCoutBois;
        m_nRessourcesPierre -= nCoutPierre;

        SetUIText();
    }

    void SetUIText()
    {
        textRessourcesBois.text = m_nRessourcesBois.ToString();
        textRessourcesPierre.text = m_nRessourcesPierre.ToString();
    }
}
