using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 隊伍狀態顯示
public class TeamStatusInterface {
    public InterfaceController _interface;
    public CharStatusInterface[] _playerChars { get; private set; }
    public CharEnemyStatusInterface[] _enemyChars { get; private set; }


    public TeamStatusInterface(InterfaceController inter) {
        _interface = inter;
    }

    public void setPlayerCharacters(CharManager[] chars) {
        _playerChars = new CharStatusInterface[chars.Length];
        for (int i = 0; i < chars.Length; i++) {
            _playerChars[i] = new CharStatusInterface(_interface, i);
        }
    }

    public void setEnemyCharacters(CharManager[] chars)
    {
        _enemyChars = new CharEnemyStatusInterface[chars.Length];
        for (int i = 0; i < chars.Length; i++) {
            _enemyChars[i] = new CharEnemyStatusInterface(_interface, i);
        }
    }

    public void update() { }

    public void showTeamRearrangeButton() {
    }
    public void hideTeamRearrangeButton() {
    }
}

// 角色狀態顯示
public class CharStatusInterface {
    public InterfaceController _interface;
    public GameObject _char { get; private set; }

    public CharStatusInterface(InterfaceController inter, int i) { 
        _interface = inter;

        _char = MonoBehaviour.Instantiate(Resources.Load("CharPlayerBar") as GameObject);
        _char.transform.position = Position.getVector3( Position.charStatusPosition[i] );
        _char.GetComponent<Button>().ButtonID = StringCoder.getChangeCharString(3);
        _char.GetComponent<BoxCollider2D>().enabled = false;

    }
    public void update() { }
}

// 敵方角色狀態顯示
public class CharEnemyStatusInterface {
    public InterfaceController _interface;
    public GameObject _char { get; private set; }
    int _pos = 0;

    public CharEnemyStatusInterface(InterfaceController inter, int i) { 
        _interface = inter;

        _char = MonoBehaviour.Instantiate(Resources.Load("CharEnemyBar") as GameObject);
        _char.transform.position = Position.getVector3(Position.charEnemyStatusPosition[i]);
        _char.GetComponent<Button>().ButtonID = StringCoder.getChangeCharString(3);
        _char.GetComponent<BoxCollider2D>().enabled = false;

    }
    public void update() { }
}