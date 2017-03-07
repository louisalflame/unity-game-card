using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public Sprite[] SpriteTexture = new Sprite[3];
    bool mouseIn = false;
    bool mouseDown = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(mouseDown.ToString()+';'+mouseIn.ToString());

    }

    void OnMouseDown()
    {
        mouseDown = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = SpriteTexture[2];
    }

    void OnMouseUp()
    {
        if (mouseIn && mouseDown)
        {
            Debug.Log("click");
        }
        mouseDown = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = SpriteTexture[1];
        //SceneManager.LoadSceneAsync("playground");
    }

    void OnMouseEnter()
    {
        mouseIn = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = SpriteTexture[1];
    }

    void OnMouseExit()
    {
        mouseIn = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = SpriteTexture[0];
    }
}
