using UnityEngine;
using System.Collections;

public class CGestionRessources : MonoBehaviour {

    int m_nRessourcesBois;
    int m_nRessourcesPierre;
    int m_nNbMainDoeuvre;
    int m_nNbMainDoeuvreDispo;

    public UILabel textRessourcesBois;
    public UILabel textRessourcesPierre;
    public UILabel textRessourcesLabor;
    public UILabel textRessourcesNotEnough;

	// Use this for initialization
	void Start () 
    {
        m_nRessourcesBois = 100;
        m_nRessourcesPierre = 20;

        m_nNbMainDoeuvre = 5;
        m_nNbMainDoeuvreDispo = m_nNbMainDoeuvre;

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

    public void AddRemoveLaborDispo(int nLabor)
    {
        m_nNbMainDoeuvreDispo += nLabor;
        SetUIText();
    }

    public int GetNbLaborDispo()
    {
        return m_nNbMainDoeuvreDispo;
    }

    void SetUIText()
    {
        textRessourcesBois.text = m_nRessourcesBois.ToString();
        textRessourcesPierre.text = m_nRessourcesPierre.ToString();
        textRessourcesLabor.text = m_nNbMainDoeuvreDispo.ToString() + "/" + m_nNbMainDoeuvre.ToString();
    }
}
