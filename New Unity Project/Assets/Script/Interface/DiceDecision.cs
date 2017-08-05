using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰面收集分配
public class AttrDecisionInterface{
    public InterfaceController _interface;
    public AttrDecisionManager _manager;
    public GameObject _attrBack { get; private set; }
    public GameObject _baseBack { get; private set; }
    private List<GameObject> _facesAttr = null;
    private List<GameObject> _facesBase = null;

    public AttrDecisionInterface(InterfaceController inter){
        _interface = inter;
        _facesAttr = new List<GameObject>();
        _facesBase = new List<GameObject>();

    }
    public void create() {
        _attrBack = CanvasFactory.createImage(_interface._canvas, "DecisionPointFace");
        CanvasFactory.setImageSprite(_attrBack, "Sprite/BackGround/decisionBack");
        CanvasFactory.setRectTransformAnchor(_attrBack, new Vector2(0f, 0.67f), new Vector2(1f, 0.82f), Vector2.zero, Vector2.zero);

        _baseBack = CanvasFactory.createImage(_interface._canvas, "DecisionBuildFace");
        CanvasFactory.setImageSprite(_baseBack, "Sprite/BackGround/decisionBack");
        CanvasFactory.setRectTransformAnchor(_baseBack, new Vector2(0f, 0.5f), new Vector2(1f, 0.65f), Vector2.zero, Vector2.zero);

        _attrBack.SetActive(false);
        _baseBack.SetActive(false);
    }
    public void initial() {
        _attrBack.SetActive(true);
        _baseBack.SetActive(true);

        AnimateWork.setAlpha(_attrBack.transform, 0f);
        AnimateWork.setAlpha(_baseBack.transform, 0f);
    }

    public void clear() {
        foreach (GameObject o in _facesAttr) { GameObject.Destroy(o); }
        foreach (GameObject o in _facesBase) { GameObject.Destroy(o); }
        _facesAttr.Clear();
        _facesBase.Clear();
    }
     
    public void showFaces()
    {
        clear();
        List<DiceFace> toAttr = _interface._battle._attrDecisionManager.getFacesAttr();
        for (int i = 0; i < toAttr.Count; i++) {
            GameObject attrFace = CanvasFactory.createButton(_attrBack, "AttrFace-" + i, StringCoder.getAttrDecisionString(i));
            CanvasFactory.setImageSprite(attrFace, toAttr[i].getImage()); 
            CanvasFactory.setRectTransformPosition(attrFace,
                new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2( countItemOffset(i, toAttr.Count), 0f), new Vector2(64, 64));
            
            _facesAttr.Add(attrFace);
        }

        List<DiceFace> toBase = _interface._battle._attrDecisionManager.getFacesBase();
        for (int i = 0; i < toBase.Count; i++) {
            GameObject baseFace = CanvasFactory.createButton(_baseBack, "BaseFace-" + i, StringCoder.getBaseDecisionString(i));
            CanvasFactory.setImageSprite(baseFace, toBase[i].getBaseImage());
            CanvasFactory.setRectTransformPosition(baseFace,
                new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2( countItemOffset(i, toBase.Count), 0f), new Vector2(64, 64));

            _facesBase.Add(baseFace);
        }
    }
    public float countItemOffset(int i, int all) {
        return (2 * i + 1 - all) * 35f;
    }

    public void hideFaceDecision() {
        AnimateWork.setAlpha(_attrBack.transform, 0f);
        AnimateWork.setAlpha(_baseBack.transform, 0f);
        clear();
    }

    public void update() { }
}