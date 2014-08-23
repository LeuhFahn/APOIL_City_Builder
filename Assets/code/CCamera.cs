using UnityEngine;
using System.Collections;

public class CCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveHorizontal(float fDeltaPosition)
    {
        transform.Translate(new Vector3(fDeltaPosition, 0, 0), Space.Self);
    }

    public void MoveVertical(float fDeltaPosition)
    {
        transform.Translate(new Vector3(-fDeltaPosition, 0, fDeltaPosition), Space.World);
    }

    public void ZoomInOut(float fDeltaPosition)
    {
        transform.Translate(new Vector3(0, 0, fDeltaPosition), Space.Self);
    }
}
