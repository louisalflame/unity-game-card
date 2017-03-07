using UnityEngine;
using System.Collections;

public class InstantiateTry : MonoBehaviour
{

    public GameObject dice;
    public GameObject newdice;

	// Use this for initialization
	void Start () {
        newdice = Instantiate(dice, new Vector3(0,0,0), new Quaternion(0, 0, 0, 0)) as GameObject;
        Instantiate(dice, new Vector3(0.2f, 0.2f, 0), new Quaternion(0, 0, 0, 0));

        newdice.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Debug.Log("created "+newdice.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
