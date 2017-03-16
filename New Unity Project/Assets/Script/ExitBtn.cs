using UnityEngine;
using System.Collections;

public class ExitBtn : MonoBehaviour {

	void Start () { }
	
	void Update () {

        if( Input.GetKey("escape") ) {
            Debug.Log(" press escape");
            Application.Quit();
        }
	}
}
