using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public float speed = 1.0f;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (GazeTimer.moveModule)
        {
            transform.Translate(Camera.main.transform.forward * Time.deltaTime * speed, Space.Self);
        }
	}
}
