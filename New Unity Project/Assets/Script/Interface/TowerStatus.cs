using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 儲存塔狀態顯示
public class TowerStatusInterface {
    public InterfaceController _interface;
    private GameObject[] _towers = null;
    public TowerStatusInterface(InterfaceController inter) { 
        _interface = inter;
        _towers = new GameObject[6] { null, null, null, null, null, null };
        _towers[0] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[0].transform.localPosition = new Vector3(3, -2, 1);
        _towers[1] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[1].transform.localPosition = new Vector3(4, -2, 1);
        _towers[2] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[2].transform.localPosition = new Vector3(5, -2, 1);
        _towers[3] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[3].transform.localPosition = new Vector3(6, -2, 1);
        _towers[4] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[4].transform.localPosition = new Vector3(7, -2, 1);
        _towers[5]= MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[5].transform.localPosition = new Vector3(8, -2, 1);
    }

    public void setTowerStatus(AttrTower[] towers) {
        for (int i = 0; i < _towers.Length && i < towers.Length; i++) {
            _towers[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( towers[i].getImage() );
        }
    }

    public void update() { }
}

//屬性點數存量顯示
public class AttrPointsInterface {
    public InterfaceController _interface;
    private GameObject[] _attrIcons = { null, null, null, null, null, null, null };
    private GameObject[] _attrNums = { null, null, null, null, null, null, null };
    public AttrPointsInterface(InterfaceController inter) {
        _interface = inter;
        _attrNums[0] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[0].transform.localPosition = new Vector3(3, -4, 1);
        _attrNums[0].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[1] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[1].transform.localPosition = new Vector3(4, -4, 1);
        _attrNums[1].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[2] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[2].transform.localPosition = new Vector3(5, -4, 1);
        _attrNums[2].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[3] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[3].transform.localPosition = new Vector3(6, -4, 1);
        _attrNums[3].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[4] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[4].transform.localPosition = new Vector3(7, -4, 1);
        _attrNums[4].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[5] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[5].transform.localPosition = new Vector3(8, -4, 1);
        _attrNums[5].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrIcons[0] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrNor");
        _attrIcons[0].transform.localPosition = new Vector3(3, -3, 1);
        _attrIcons[1] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrAtk");
        _attrIcons[1].transform.localPosition = new Vector3(4, -3, 1);
        _attrIcons[2] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrDef");
        _attrIcons[2].transform.localPosition = new Vector3(5, -3, 1);
        _attrIcons[3] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrMov");
        _attrIcons[3].transform.localPosition = new Vector3(6, -3, 1);
        _attrIcons[4] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrSpc");
        _attrIcons[4].transform.localPosition = new Vector3(7, -3, 1);
        _attrIcons[5] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrHeal");
        _attrIcons[5].transform.localPosition = new Vector3(8, -3, 1);
    }

    public void setAttrNums(int[] attrNums) {
        for (int i = 0; i < attrNums.Length && i < _attrNums.Length; i++) {
            _attrNums[i].transform.Find("num").GetComponent<TextMesh>().text = attrNums[i].ToString();
        }
    }
    public void setAttrNum(int attr, int num) {
        _attrNums[attr].transform.Find("num").GetComponent<TextMesh>().text = num.ToString();
    }

    public void update() { }
}
