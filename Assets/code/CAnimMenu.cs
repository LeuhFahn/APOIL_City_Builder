using UnityEngine;
using System.Collections;

public class CAnimMenu : MonoBehaviour {

	public bool m_bNegatif;
	public bool m_bRotation;
    Vector2 fPositionFinal;
    Vector2 fPositionDepart;
    Vector2 tmpPosition;
    float fTimer;
    float fTimerMax;

	// Use this for initialization
	void Start () {
        fPositionFinal = Vector2.zero;
        tmpPosition = Vector2.zero;
        fTimer = 0.0f;
        fTimerMax = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(m_bNegatif)
		{
	        if (fTimer >= fTimerMax)
	        {
	            fPositionDepart = fPositionFinal;
	            fPositionFinal = Random.insideUnitCircle * 5.0f;
	            fTimer = 0.0f;
	            fTimerMax = Random.insideUnitCircle.x * 10.0f;
	        }
	        
	        tmpPosition.x = CApoilMath.InterpolationLinear(fTimer, 0.0f, fTimerMax, fPositionDepart.x, fPositionFinal.x);
	        tmpPosition.y = CApoilMath.InterpolationLinear(fTimer, 0.0f, fTimerMax, fPositionDepart.y, fPositionFinal.y);

	        transform.localPosition = new Vector3(tmpPosition.x, tmpPosition.y, 0);

	        fTimer += Time.deltaTime;
		}

		if(m_bRotation)
		{
			transform.Rotate(0, Mathf.Cos (Time.time) * 10.0f/180.0f,0);
		}
	}
}
