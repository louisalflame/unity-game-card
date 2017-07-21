using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// 儲存塔狀態顯示
public class TowerStatusInterface {
    public InterfaceController _interface;
    public GameObject _towerTable { get; private set; }
    public GameObject[] _towers { get; private set; }

    public TowerStatusInterface(InterfaceController inter) { 
        _interface = inter;

        Dictionary<string, GameObject[]> dict = CanvasFactory.create_BattleScene_PlayerTowerStatus(_interface.getImageRightMenu());

        _towerTable = dict["TowerTable"][0];
        _towers = dict["TowerIcons"];
    }

    public void setTowerStatus(AttrTower[] towers) {
        for (int i = 0; i < _towers.Length && i < towers.Length; i++) {
            _towers[i].GetComponent<Image>().sprite = Resources.Load<Sprite>( towers[i].getImage() );
        }
    }

    public void update() { }
}

//屬性點數存量顯示
public class AttrPointsInterface {
    public InterfaceController _interface;
    public GameObject _pointTable { get; private set; }
    public GameObject[] _attrIcons { get; private set; }
    public GameObject[] _attrNums { get; private set; }

    public AttrPointsInterface(InterfaceController inter) {
        _interface = inter;

        Dictionary<string, GameObject[]> dict = CanvasFactory.create_BattleScene_PlayerPointStatus(_interface.getImageRightMenu());

        _pointTable = dict["PointTable"][0];
        _attrIcons = dict["PointAttrs"];
        _attrNums = dict["PointTexts"];

    }

    public void setAttrNums(int[] attrNums) {
        for (int i = 0; i < attrNums.Length && i < _attrNums.Length; i++) {
            _attrNums[i].GetComponent<Text>().text = attrNums[i].ToString();
        }
    }
    public void setAttrNum(int attr, int num) {
        _attrNums[attr].GetComponent<Text>().text = num.ToString();
    }

    public void update() { }
}


// 敵方儲存塔狀態顯示
public class TowerStatusEnemyInterface {
    public InterfaceController _interface;
    public GameObject _towerTable { get; private set; }
    public GameObject[] _towers { get; private set; }

    public TowerStatusEnemyInterface(InterfaceController inter) { 
        _interface = inter;

        CanvasFactory.create_BattleScene_EnemyTowerStatus(_interface.getImageEnemyTableMask());


        _towers = new GameObject[6] { null, null, null, null, null, null };
    }

    public void setTowerStatus(AttrTower[] towers) {
      //  for (int i = 0; i < _towers.Length && i < towers.Length; i++) {
      //      _towers[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( towers[i].getImage() );
       // }
    }
}
// 敵方屬性點狀態顯示
public class AttrPointsEnemyInterface {
    public InterfaceController _interface;
    public GameObject _pointTable { get; private set; }
    public GameObject[] _attrIcons { get; private set; }
    public GameObject[] _attrNums { get; private set; }

    public AttrPointsEnemyInterface(InterfaceController inter) {
        _interface = inter;

        CanvasFactory.create_BattleScene_EnemyPointStatus(_interface.getImageEnemyTableMask());
        
        _attrIcons = new GameObject[6] { null, null, null, null, null, null };

        _attrNums = new GameObject[6] { null, null, null, null, null, null };
    }

    public void setAttrNums(int[] attrNums) {
       // for (int i = 0; i < attrNums.Length && i < _attrNums.Length; i++) {
       //     _attrNums[i].GetComponent<TextMesh>().text = attrNums[i].ToString();
       // }
    }
    public void setAttrNum(int attr, int num) {
       // _attrNums[attr].transform.Find("num").GetComponent<TextMesh>().text = num.ToString();
    }
}