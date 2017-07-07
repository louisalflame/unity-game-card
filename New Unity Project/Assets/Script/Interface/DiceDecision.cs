using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰面收集分配
public class AttrDecisionInterface{
    public InterfaceController _interface;
    public AttrDecisionManager _manager;
    private GameObject _attrBack = null;
    private GameObject _baseBack = null;
    private List<GameObject> _facesAttr = null;
    private List<GameObject> _facesBase = null;

    public AttrDecisionInterface(InterfaceController inter){
        _interface = inter;
        _facesAttr = new List<GameObject>();
        _facesBase = new List<GameObject>();

        _attrBack = MonoBehaviour.Instantiate(Resources.Load("DecisionBack") as GameObject);
        _attrBack.transform.localPosition = Position.getVector3( Position.decisionAttrBackPosition );
        _attrBack.SetActive(false);
        _baseBack = MonoBehaviour.Instantiate(Resources.Load("DecisionBack") as GameObject);
        _baseBack.transform.localPosition = Position.getVector3( Position.decisionBaseBackPosition );
        _baseBack.SetActive(false);
    }

    public void clear() {
        foreach (GameObject o in _facesAttr) { GameObject.Destroy(o); }
        foreach (GameObject o in _facesBase) { GameObject.Destroy(o); }
        _facesAttr.Clear();
        _facesBase.Clear();
    }

    public void showFaces() {
        clear();
        _attrBack.SetActive(true);
        List<DiceFace> toAttr = _interface._battle._attrDecisionManager.getFacesAttr();
        for (int i = 0; i < toAttr.Count; i++) {
            GameObject attrFace = MonoBehaviour.Instantiate(Resources.Load("FaceDecisionBtn") as GameObject);
            attrFace.transform.parent = _attrBack.transform;
            attrFace.transform.localPosition = Position.getDiceFaceDecisionPosition(i, toAttr.Count);
            attrFace.transform.localScale = Position.getVector3(Position.diceScale);
            attrFace.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(toAttr[i].getImage());
            attrFace.GetComponent<Button>().ButtonID = StringCoder.getAttrDecisionString(i);
            _facesAttr.Add(attrFace);
        }

        _baseBack.SetActive(true);
        List<DiceFace> toBase = _interface._battle._attrDecisionManager.getFacesBase();
        for (int i = 0; i < toBase.Count; i++) {
            GameObject baseFace = MonoBehaviour.Instantiate(Resources.Load("FaceDecisionBtn") as GameObject);
            baseFace.transform.parent = _baseBack.transform;
            baseFace.transform.localPosition = Position.getDiceBaseDesicionPosition(i, toBase.Count);
            baseFace.transform.localScale = Position.getVector3(Position.diceScale);
            baseFace.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(toBase[i].getBaseImage());
            baseFace.GetComponent<Button>().ButtonID = StringCoder.getBaseDecisionString(i);
            _facesBase.Add(baseFace);
        }
    }

    public void hideFaceDecision() {
        _attrBack.SetActive(false);
        _baseBack.SetActive(false);
        clear();
    }

    public void update() { }
}