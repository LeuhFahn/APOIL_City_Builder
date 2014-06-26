using UnityEngine;
using System.Collections;

public class CGestionMenuMainDoeuvre : MonoBehaviour {

    GameObject m_caseSelected;
    public UILabel label_nbDeltaMainDoeuvre;
    public UILabel label_nbMainDoeuvre;

    int m_nDeltaNbLabor;
    int m_nNbLabor;

	// Use this for initialization
	void Start () {
        m_nDeltaNbLabor = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (gameObject.GetComponent<Game>().menuMainDoeuvre.activeSelf == true)
        {
            m_nDeltaNbLabor += (int)(10.0f * Input.GetAxis("Mouse ScrollWheel"));

            label_nbDeltaMainDoeuvre.text = m_nDeltaNbLabor.ToString();
            label_nbMainDoeuvre.text = m_nNbLabor.ToString();
        }
	}

    public void OpenMenu(GameObject caseToSelect)
    {
        NGUITools.SetActive(gameObject.GetComponent<Game>().menuMainDoeuvre, true);
        SetCaseSelected(caseToSelect);
        if (m_caseSelected.GetComponent<hex>().GetBatiment() != null)
        {
            m_caseSelected.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.green;
            m_nDeltaNbLabor = 0;
            m_nNbLabor = m_caseSelected.GetComponent<hex>().GetBatiment().GetComponent<CBatiment>().GetNbLabor();
        }
        else
            CloseMenu();
    }

    public void CloseMenu()
    {
        NGUITools.SetActive(gameObject.GetComponent<Game>().menuMainDoeuvre, false);
        m_caseSelected.transform.FindChild("render").GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ValidateAndClose()
    {
        m_caseSelected.GetComponent<hex>().GetBatiment().GetComponent<CBatiment>().SetNbLabor(m_nNbLabor + m_nDeltaNbLabor);
        CloseMenu();
    }

    void SetCaseSelected(GameObject caseToSelect)
    {
        m_caseSelected = caseToSelect;
    }
}
