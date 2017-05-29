using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public string ButtonID;
    public Sprite[] buttonSprite;

    bool mouseIn = false;
    bool mouseDown = false;

	void Start () {
        if (buttonSprite.Length > 0) { 
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[0]; 
        }
    }	
	void Update () { }

    void OnMouseDown() {
        mouseDown = true;
        if (buttonSprite.Length > 0) { 
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[2]; 
        }
    }
    void OnMouseUp() {
        if (mouseDown && mouseIn)
        {
            Debug.Log("clicked button " + ButtonID);
            InputController.Inputs.addInput(ButtonID);
        }
        mouseDown = false;
        if (buttonSprite.Length > 0) { 
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[1]; 
        }
    }
    void OnMouseEnter() {
        mouseIn = true;
        if (buttonSprite.Length > 0) { 
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[1]; 
        }
    }
    void OnMouseExit() {
        mouseIn = false;
        if (buttonSprite.Length > 0) { 
            gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite[0]; 
        }
    }
}
