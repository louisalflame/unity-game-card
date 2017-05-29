using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰面收集分配
public class AttrDecisionInterface{
    public InterfaceController _interface;
    public AttrDecisionManager _manager;
    private List<GameObject> _facesAttr = null;
    private List<GameObject> _facesBase = null;
    public AttrDecisionInterface(InterfaceController inter){
        _interface = inter;
        _facesAttr = new List<GameObject>();
        _facesBase = new List<GameObject>();
    }
    public void setAttrDecision(AttrDecisionManager manager) { _manager = manager; }

    public void clear() {
        foreach (GameObject o in _facesAttr) { MonoBehaviour.Destroy(o); }
        foreach (GameObject o in _facesBase) { MonoBehaviour.Destroy(o); }
        _facesAttr = new List<GameObject>();
        _facesBase = new List<GameObject>();
    }
    public void showFaces() {
        clear();
        List<DiceFace> toAttr = _manager.getFacesAttr();
        for (int i = 0; i < toAttr.Count; i++) {
            GameObject sprite = MonoBehaviour.Instantiate(Resources.Load("FaceDecisionBtn") as GameObject);
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( toAttr[i].getImage() );
            sprite.GetComponent<Button>().ButtonID = StringCoder.getAttrDecisionString(i);
            sprite.transform.localPosition = new Vector3(-5 + i * 1.2f, 0.5f, -2);
            _facesAttr.Add(sprite);
        }
        List<DiceFace> toBase = _manager.getFacesBase();
        for (int i = 0; i < toBase.Count; i++) {
            GameObject sprite = MonoBehaviour.Instantiate(Resources.Load("FaceDecisionBtn") as GameObject);
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( toBase[i].getBaseImage() );
            sprite.GetComponent<Button>().ButtonID = StringCoder.getBaseDecisionString(i);
            sprite.transform.localPosition = new Vector3(-5 + i * 1.2f, -0.5f, -2);
            _facesBase.Add(sprite);
        }
    }

    public void update() { }
}