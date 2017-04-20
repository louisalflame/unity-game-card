using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public string ButtonID;
    bool mouseIn = false;
    bool mouseDown = false;

	void Start () { }	
	void Update () { }

    void OnMouseDown() {
        mouseDown = true;
        // gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[2];
    }
    void OnMouseUp() {
        if (mouseDown && mouseIn)
        {
            Debug.Log("clicked button " + ButtonID);
            InputController.Inputs.addInput(ButtonID);
        }
        mouseDown = false;
        // gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[1];
    }
    void OnMouseEnter() {
        mouseIn = true;
        // gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[1];
    }
    void OnMouseExit() {
        mouseIn = false;
        // gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[0];
    }
}
