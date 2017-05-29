using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 隊伍狀態顯示
public class TeamStatusInterface {
    public InterfaceController _interface;
    GameObject _char1 = null;
    GameObject _char2 = null;
    GameObject _char3 = null;
    public TeamStatusInterface(InterfaceController inter) {
        _interface = inter;
        _char1 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char1.transform.position = new Vector3(-4.5f, -3.5f, -1);
        _char1.transform.localScale = new Vector3(2, 2, 1);
        _char1.GetComponent<BoxCollider2D>().enabled = false;
        _char2 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char2.transform.position = new Vector3(-1, -4, -1);
        _char2.transform.localScale = new Vector3(1, 1, 1);
        _char2.GetComponent<Button>().ButtonID = StringCoder.getChangeCharString(1);
        _char2.GetComponent<BoxCollider2D>().enabled = false;
        _char3 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char3.transform.position = new Vector3(1.5f, -4, -1);
        _char3.transform.localScale = new Vector3(1, 1, 1);
        _char3.GetComponent<Button>().ButtonID = StringCoder.getChangeCharString(2);
        _char3.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void update() { }

    public void showTeamRearrangeButton() {
        _char2.GetComponent<BoxCollider2D>().enabled = true;
        _char3.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void hideTeamRearrangeButton() {
        _char2.GetComponent<BoxCollider2D>().enabled = false;
        _char3.GetComponent<BoxCollider2D>().enabled = false;
    }
}
// 角色狀態顯示
public class CharStatusInterface {
    public InterfaceController _interface;
    GameObject character = null;
    public CharStatusInterface(InterfaceController inter) { 
        _interface = inter;
    }
    public void update() { }
}