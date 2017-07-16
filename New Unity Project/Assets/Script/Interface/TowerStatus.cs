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

        Dictionary<string, GameObject[]> dict = CanvasFactory.create_BattleScene_PlayerTowerStatus(_interface._menuButton._mainBase);

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

        Dictionary<string, GameObject[]> dict = CanvasFactory.create_BattleScene_PlayerPointStatus(_interface._menuButton._mainBase);

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

        _towerTable = MonoBehaviour.Instantiate(Resources.Load("TowerTable") as GameObject);
        _towerTable.transform.localPosition = Position.getVector3(Position.towerTableEnemy);

        _towers = new GameObject[6] { null, null, null, null, null, null };
        _towers[0] = _towerTable.transform.Find("towerGround1/base").gameObject;
        _towers[1] = _towerTable.transform.Find("towerGround2/base").gameObject;
        _towers[2] = _towerTable.transform.Find("towerGround3/base").gameObject;
        _towers[3] = _towerTable.transform.Find("towerGround4/base").gameObject;
        _towers[4] = _towerTable.transform.Find("towerGround5/base").gameObject;
        _towers[5] = _towerTable.transform.Find("towerGround6/base").gameObject;
    }

    public void setTowerStatus(AttrTower[] towers) {
        for (int i = 0; i < _towers.Length && i < towers.Length; i++) {
            _towers[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( towers[i].getImage() );
        }
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

        _pointTable = MonoBehaviour.Instantiate(Resources.Load("PointTable") as GameObject);
        _pointTable.transform.localPosition = Position.getVector3(Position.pointTable);

        _attrIcons = new GameObject[6] { null, null, null, null, null, null };
        _attrIcons[0] = _pointTable.transform.Find("pointTableNor/AttrNor").gameObject;
        _attrIcons[1] = _pointTable.transform.Find("pointTableAtk/AttrAtk").gameObject;
        _attrIcons[2] = _pointTable.transform.Find("pointTableDef/AttrDef").gameObject;
        _attrIcons[3] = _pointTable.transform.Find("pointTableMov/AttrMov").gameObject;
        _attrIcons[4] = _pointTable.transform.Find("pointTableSpc/AttrSpc").gameObject;
        _attrIcons[5] = _pointTable.transform.Find("pointTableHeal/AttrHeal").gameObject;

        _attrNums = new GameObject[6] { null, null, null, null, null, null };
        _attrNums[0] = _pointTable.transform.Find("pointTableNor/numBase/num").gameObject;
        _attrNums[1] = _pointTable.transform.Find("pointTableAtk/numBase/num").gameObject;
        _attrNums[2] = _pointTable.transform.Find("pointTableDef/numBase/num").gameObject;
        _attrNums[3] = _pointTable.transform.Find("pointTableMov/numBase/num").gameObject;
        _attrNums[4] = _pointTable.transform.Find("pointTableSpc/numBase/num").gameObject;
        _attrNums[5] = _pointTable.transform.Find("pointTableHeal/numBase/num").gameObject;
    }

    public void setAttrNums(int[] attrNums) {
        for (int i = 0; i < attrNums.Length && i < _attrNums.Length; i++) {
            _attrNums[i].GetComponent<TextMesh>().text = attrNums[i].ToString();
        }
    }
    public void setAttrNum(int attr, int num) {
        _attrNums[attr].transform.Find("num").GetComponent<TextMesh>().text = num.ToString();
    }
}