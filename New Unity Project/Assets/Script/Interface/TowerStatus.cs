using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

// 儲存塔狀態顯示
public class TowerStatusInterface {
    public InterfaceController _interface;
    public GameObject _towerTable { get; private set; }
    public GameObject[] _towers { get; private set; }

    public TowerStatusInterface(InterfaceController inter) { _interface = inter; }
    public void create() { 
         create_BattleScene_PlayerTowerStatus(_interface.getImageRightMenu()); 
    }
    public void initial() {
        _towerTable.SetActive(true);
        AnimateWork.setAlpha(_towerTable.transform, 0);
    }

    public void setTowerStatus(AttrTower[] towers) {
        for (int i = 0; i < _towers.Length && i < towers.Length; i++) {
            CanvasFactory.setImageSprite(_towers[i], towers[i].getImage());
        }
    }

    public void update() { }

    // Menu Tower Status *************
    public void create_BattleScene_PlayerTowerStatus(GameObject parent) {
        _towerTable = CanvasFactory.createEmptyRect(parent, "PlayerTowerStatus");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width - 20, 280f);
        CanvasFactory.setRectTransformPosition(_towerTable, new Vector2(0.05f, 0.65f), new Vector2(0.05f, 0.65f), Vector2.zero, new Vector2(width, width / 6));
        CanvasFactory.setRectPivot(_towerTable, new Vector2(0f, 0f));

        CanvasFactory.setRatioFitterWidthControlsHeight(_towerTable, 6f);

        GameObject t1Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t1Obj, new Vector2(0f, 0f), new Vector2(1f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t2Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t2Obj, new Vector2(1f / 6, 0f), new Vector2(2f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t3Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t3Obj, new Vector2(2f / 6, 0f), new Vector2(3f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t4Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t4Obj, new Vector2(3f / 6, 0f), new Vector2(4f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t5Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t5Obj, new Vector2(4f / 6, 0f), new Vector2(5f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t6Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t6Obj, new Vector2(5f / 6, 0f), new Vector2(6f / 6, 1f), Vector2.zero, Vector2.zero);

        _towers = new GameObject[] {
            t1Obj.transform.Find("TowerIcon").gameObject,
            t2Obj.transform.Find("TowerIcon").gameObject,
            t3Obj.transform.Find("TowerIcon").gameObject,
            t4Obj.transform.Find("TowerIcon").gameObject,
            t5Obj.transform.Find("TowerIcon").gameObject,
            t6Obj.transform.Find("TowerIcon").gameObject
        };

        _towerTable.SetActive(false);
    }
}

//屬性點數存量顯示
public class AttrPointsInterface {
    public InterfaceController _interface;
    public GameObject _pointTable { get; private set; }
    public GameObject[] _attrIcons { get; private set; }
    public GameObject[] _attrNums { get; private set; }

    public AttrPointsInterface(InterfaceController inter) { _interface = inter; }
    public void create(){
        create_BattleScene_PlayerPointStatus(_interface.getImageRightMenu()); 
    }
    public void initial() {
        _pointTable.SetActive(true);
        AnimateWork.setAlpha(_pointTable.transform, 0);
    }

    public void setAttrNums(int[] attrNums) {
        for (int i = 0; i < attrNums.Length && i < _attrNums.Length; i++) {
            CanvasFactory.setTextString(_attrNums[i], attrNums[i].ToString());
        }
    }
    public void setAttrNum(int attr, int num) {
        CanvasFactory.setTextString(_attrNums[attr], num.ToString());
    }

    public void update() { }
      
    // Menu Point Status *************
    public void create_BattleScene_PlayerPointStatus(GameObject parent) {
        _pointTable = CanvasFactory.createEmptyRect(parent, "PlayerPointStatus");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width - 20, 280f);
        CanvasFactory.setRectTransformPosition(_pointTable, new Vector2(0.05f, 0.6f), new Vector2(0.05f, 0.6f), Vector2.zero, new Vector2(width, width / 3));
        CanvasFactory.setRectPivot(_pointTable, new Vector2(0f, 1f));

        CanvasFactory.setRatioFitterWidthControlsHeight(_pointTable, 3f);

        GameObject norObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrNor", "Sprite/PointTable/pointTableNor", "Sprite/AttrIcon/AttrNor");
        CanvasFactory.setRectTransformAnchor(norObj, new Vector2(0f, 0f), new Vector2(1f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject atkObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrAtk", "Sprite/PointTable/pointTableAtk", "Sprite/AttrIcon/AttrAtk");
        CanvasFactory.setRectTransformAnchor(atkObj, new Vector2(1f / 6, 0f), new Vector2(2f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject defObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrDef", "Sprite/PointTable/pointTableDef", "Sprite/AttrIcon/AttrDef");
        CanvasFactory.setRectTransformAnchor(defObj, new Vector2(2f / 6, 0f), new Vector2(3f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject movObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrMov", "Sprite/PointTable/pointTableMov", "Sprite/AttrIcon/AttrMov");
        CanvasFactory.setRectTransformAnchor(movObj, new Vector2(3f / 6, 0f), new Vector2(4f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject spcObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrSpc", "Sprite/PointTable/pointTableSpc", "Sprite/AttrIcon/AttrSpc");
        CanvasFactory.setRectTransformAnchor(spcObj, new Vector2(4f / 6, 0f), new Vector2(5f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject healObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrHeal", "Sprite/PointTable/pointTableHeal", "Sprite/AttrIcon/AttrHeal");
        CanvasFactory.setRectTransformAnchor(healObj, new Vector2(5f / 6, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        _attrIcons = new GameObject[] { norObj, atkObj, defObj, movObj, spcObj, healObj };
        _attrNums = new GameObject[] {
                norObj.transform.Find("num/txt").gameObject,
                atkObj.transform.Find("num/txt").gameObject,
                defObj.transform.Find("num/txt").gameObject,
                movObj.transform.Find("num/txt").gameObject,
                spcObj.transform.Find("num/txt").gameObject,
                healObj.transform.Find("num/txt").gameObject,
            };

        _pointTable.SetActive(false);
    }
}


// 敵方儲存塔狀態顯示
public class TowerStatusEnemyInterface {
    public InterfaceController _interface;
    public GameObject _towerTable { get; private set; }
    public GameObject[] _towers { get; private set; }

    public TowerStatusEnemyInterface(InterfaceController inter) { _interface = inter; }
    public void create() {
        create_BattleScene_EnemyTowerStatus(_interface.getImageEnemyTableMask()); 
    }
    public void initial() {
        _towerTable.SetActive(true);
        AnimateWork.setAlpha(_towerTable.transform, 0);
    }

    public void setTowerStatus(AttrTower[] towers) {
        for (int i = 0; i < _towers.Length && i < towers.Length; i++) {
            CanvasFactory.setImageSprite(_towers[i], towers[i].getImage());
        }
    }

    public void update() { }

    // Enemy Tower Status *************
    public void create_BattleScene_EnemyTowerStatus(GameObject parent) {
        _towerTable = CanvasFactory.createButton(parent, "EnemyTowerStatus", "");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width - 20, 280f);
        CanvasFactory.setRectTransformPosition(_towerTable, new Vector2(0.05f, 1f), new Vector2(0.05f, 1f), Vector2.zero, new Vector2(width, width / 6));
        CanvasFactory.setRectPivot(_towerTable, new Vector2(0f, 1f));

        CanvasFactory.setRatioFitterWidthControlsHeight(_towerTable, 6f);

        CanvasFactory.addPointerClickCallback(_towerTable, (e) => {
            _interface._attrPointsEnemy.toggleLookUp();
        });

        GameObject t1Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t1Obj, new Vector2(0f, 0f), new Vector2(1f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t2Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t2Obj, new Vector2(1f / 6, 0f), new Vector2(2f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t3Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t3Obj, new Vector2(2f / 6, 0f), new Vector2(3f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t4Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t4Obj, new Vector2(3f / 6, 0f), new Vector2(4f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t5Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t5Obj, new Vector2(4f / 6, 0f), new Vector2(5f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject t6Obj = CanvasFactory.create_TowerStatus_Unit(_towerTable);
        CanvasFactory.setRectTransformAnchor(t6Obj, new Vector2(5f / 6, 0f), new Vector2(6f / 6, 1f), Vector2.zero, Vector2.zero);

        _towers = new GameObject[] {
            t1Obj.transform.Find("TowerIcon").gameObject,
            t2Obj.transform.Find("TowerIcon").gameObject,
            t3Obj.transform.Find("TowerIcon").gameObject,
            t4Obj.transform.Find("TowerIcon").gameObject,
            t5Obj.transform.Find("TowerIcon").gameObject,
            t6Obj.transform.Find("TowerIcon").gameObject
        };

        _towerTable.SetActive(false);
    }
}
// 敵方屬性點狀態顯示
public class AttrPointsEnemyInterface {
    public InterfaceController _interface;
    public GameObject _pointTable { get; private set; }
    public GameObject[] _attrIcons { get; private set; }
    public GameObject[] _attrNums { get; private set; }

    private enum Mode { ready=1, opening, show, closing }
    private Mode _mode = Mode.ready;
    private float _openingSpeed = 8f;
    private float _closingSpeed = 10f;
    private float _distance;

    public AttrPointsEnemyInterface(InterfaceController inter) { _interface = inter; }
    public void create() {
        create_BattleScene_EnemyPointStatus(_interface.getImageEnemyTableMask()); 
    }
    public void initial() {
        _pointTable.SetActive(true);
        AnimateWork.setAlpha(_pointTable.transform, 0);
    }

    public void setAttrNums(int[] attrNums) {
        for (int i = 0; i < attrNums.Length && i < _attrNums.Length; i++) {
            CanvasFactory.setTextString(_attrNums[i], attrNums[i].ToString());
        }
    }
    public void setAttrNum(int attr, int num) {
        CanvasFactory.setTextString(_attrNums[attr], num.ToString());
    }

    public void toggleLookUp() {
        if (_mode == Mode.ready || _mode == Mode.closing) _mode = Mode.opening;
        else if (_mode == Mode.opening || _mode == Mode.show) _mode = Mode.closing;
    }
    public void update() {
        switch (_mode) {
            case Mode.ready: break;
            case Mode.opening:
                if (_pointTable.GetComponent<RectTransform>().anchoredPosition.y > _distance) {
                    float movingY = _pointTable.GetComponent<RectTransform>().anchoredPosition.y;
                    movingY = Mathf.Max(_distance, movingY - _openingSpeed);
                    _pointTable.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, movingY);
                }else { _mode = Mode.show; }
                break;
            case Mode.show: break;
            case Mode.closing:
                if (_pointTable.GetComponent<RectTransform>().anchoredPosition.y < 0f) {
                    float movingY = _pointTable.GetComponent<RectTransform>().anchoredPosition.y;
                    movingY = Mathf.Min(0f, movingY + _closingSpeed);
                    _pointTable.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, movingY);
                }else { _mode = Mode.ready; }
                break;
        }
    }

    // Enemy Point Status *************
    public void create_BattleScene_EnemyPointStatus(GameObject parent) {
        _pointTable = CanvasFactory.createButton(parent, "EnemyPointStatus", "");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width - 20, 280f);
        CanvasFactory.setRectTransformPosition(_pointTable, new Vector2(0.05f, 1f), new Vector2(0.05f, 1f), Vector2.zero, new Vector2(width, width / 3));
        CanvasFactory.setRectPivot(_pointTable, new Vector2(0f, 0f));

        CanvasFactory.setRatioFitterWidthControlsHeight(_pointTable, 3f);
        _distance = _pointTable.GetComponent<RectTransform>().rect.height * -1.5f;

        CanvasFactory.addPointerClickCallback(_pointTable, (e) => { toggleLookUp(); });

        GameObject norObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrNor", "Sprite/PointTable/pointTableNor", "Sprite/AttrIcon/AttrNor");
        CanvasFactory.setRectTransformAnchor(norObj, new Vector2(0f, 0f), new Vector2(1f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject atkObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrAtk", "Sprite/PointTable/pointTableAtk", "Sprite/AttrIcon/AttrAtk");
        CanvasFactory.setRectTransformAnchor(atkObj, new Vector2(1f / 6, 0f), new Vector2(2f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject defObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrDef", "Sprite/PointTable/pointTableDef", "Sprite/AttrIcon/AttrDef");
        CanvasFactory.setRectTransformAnchor(defObj, new Vector2(2f / 6, 0f), new Vector2(3f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject movObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrMov", "Sprite/PointTable/pointTableMov", "Sprite/AttrIcon/AttrMov");
        CanvasFactory.setRectTransformAnchor(movObj, new Vector2(3f / 6, 0f), new Vector2(4f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject spcObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrSpc", "Sprite/PointTable/pointTableSpc", "Sprite/AttrIcon/AttrSpc");
        CanvasFactory.setRectTransformAnchor(spcObj, new Vector2(4f / 6, 0f), new Vector2(5f / 6, 1f), Vector2.zero, Vector2.zero);
        GameObject healObj = CanvasFactory.create_PointStatus_Unit(
            _pointTable, "attrHeal", "Sprite/PointTable/pointTableHeal", "Sprite/AttrIcon/AttrHeal");
        CanvasFactory.setRectTransformAnchor(healObj, new Vector2(5f / 6, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        _attrIcons = new GameObject[] { norObj, atkObj, defObj, movObj, spcObj, healObj };
        _attrNums = new GameObject[] {
                norObj.transform.Find("num/txt").gameObject,
                atkObj.transform.Find("num/txt").gameObject,
                defObj.transform.Find("num/txt").gameObject,
                movObj.transform.Find("num/txt").gameObject,
                spcObj.transform.Find("num/txt").gameObject,
                healObj.transform.Find("num/txt").gameObject,
            };

        _pointTable.SetActive(false);
    }
}