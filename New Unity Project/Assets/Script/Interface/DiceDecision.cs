using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰面收集分配
public class AttrDecisionInterface{
    public InterfaceController _interface;
    public AttrDecisionManager _manager;
    public GameObject _attrBack { get; private set; }
    public GameObject _baseBack { get; private set; }
    private List<DiceFace> _toAttr = null;
    private List<DiceFace> _toBase = null;
    private List<GameObject> _facesAttr = null;
    private List<GameObject> _facesBase = null;

    public AttrDecisionInterface(InterfaceController inter){
        _interface = inter;
        _facesAttr = new List<GameObject>();
        _facesBase = new List<GameObject>();

    }
    public void create() {
        _baseBack = CanvasFactory.createImage(_interface.getImageDecisionField(), "DecisionBuildFace");
        CanvasFactory.setImageSprite(_baseBack, "Sprite/BackGround/decisionBack");
        CanvasFactory.setRectTransformAnchor(_baseBack, new Vector2(0f, 0.5f), new Vector2(1f, 0.65f), Vector2.zero, Vector2.zero);

        _attrBack = CanvasFactory.createImage(_interface.getImageDecisionField(), "DecisionPointFace");
        CanvasFactory.setImageSprite(_attrBack, "Sprite/BackGround/decisionBack");
        CanvasFactory.setRectTransformAnchor(_attrBack, new Vector2(0f, 0.67f), new Vector2(1f, 0.82f), Vector2.zero, Vector2.zero);

        _attrBack.SetActive(false);
        _baseBack.SetActive(false);
    }
    public void initial() {
        _attrBack.SetActive(true);
        _baseBack.SetActive(true);

        AnimateWork.setAlpha(_attrBack.transform, 0f);
        AnimateWork.setAlpha(_baseBack.transform, 0f);
    }

    public void clearAttr() {
        foreach (GameObject o in _facesAttr) { GameObject.Destroy(o); }
        _facesAttr.Clear();
    }
    public void clearBase() {
        foreach (GameObject o in _facesBase) { GameObject.Destroy(o); }
        _facesBase.Clear();
    }
     
    public void showFaces() {
        clearAttr();
        clearBase();
        _toAttr = _interface._battle._attrDecisionManager.getFacesAttr();
        for (int i = 0; i < _toAttr.Count; i++) {
            GameObject attrFace = CanvasFactory.createButton(_attrBack, "AttrFace-" + i, StringCoder.getAttrDecisionString(i));
            CanvasFactory.setImageSprite(attrFace, _toAttr[i].getImage()); 
            CanvasFactory.setRectTransformPosition(attrFace,
                new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(countItemOffset(i, _toAttr.Count), 0f), new Vector2(64, 64));
            
            _facesAttr.Add(attrFace);
        }

        _toBase = _interface._battle._attrDecisionManager.getFacesBase();
        for (int i = 0; i < _toBase.Count; i++) {
            GameObject baseFace = CanvasFactory.createButton(_baseBack, "BaseFace-" + i, StringCoder.getBaseDecisionString(i));
            CanvasFactory.setImageSprite(baseFace, _toBase[i].getBaseImage());
            CanvasFactory.setRectTransformPosition(baseFace,
                new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(countItemOffset(i, _toBase.Count), 0f), new Vector2(64, 64));

            _facesBase.Add(baseFace);
        }
    }
    public float countItemOffset(int i, int all) {
        return (2 * i + 1 - all) * 35f;
    }

    public AnimateWork[] getAttrAggregateAnimate() {
        GameObject[] pointIcons = _interface._attrPoints._attrIcons;
        List<AnimateWork> animates = new List<AnimateWork>();

        for (int i = 0; i < _facesAttr.Count && i < _toAttr.Count; i++) {
            GameObject face = _facesAttr[i];
            DiceFace attrFace = _toAttr[i];
            GameObject icon = pointIcons[ attrFace._attr ];
            Vector3 start = face.transform.position;
            Vector3 target = icon.transform.position;
            Rect rect = icon.GetComponent<RectTransform>().rect;

            AnimateWork moveToAttr = new AnimateCurveMoveToPosition( face.transform, new Vector3[2] { start, target }, 40);
            moveToAttr.setCombineWork(
                    new AnimateRectTo(face.GetComponent<RectTransform>(), rect, 40)
                ).setWaitBefore(i * 5);
            // 注意 combine work和next work 互衝，要分開設定
            moveToAttr.setWaitBefore(i * 5).setEnd(() => {
                    CanvasFactory.setImageSpriteTo(face, icon); 
                    _interface.addAttrNum( attrFace._attr,  attrFace._num ); }
                ).setNextWork(
                    new AnimateFadeOut(face.transform, 30)
                ).setEnd( () => {
                    face.SetActive(false); }
                ).setCombineWork(
                    new AnimateRectTo(face.GetComponent<RectTransform>(), new Vector3(2, 2, 1), 30)
                );

            animates.Add( moveToAttr );
        }
        return animates.ToArray();
    }
    public AnimateWorker getAttrBackFadeAnimate() {
        return new AnimateWorker(
                new AnimateFadeOut(_attrBack.transform, 20)
            ).setEnd( () => {
                clearAttr(); }
            );
    }

    public AnimateWork[] getBaseAggregateAnimate() {
        GameObject buildIcon = _interface._towerStatus.getBuildTower();
        List<AnimateWork> animates = new List<AnimateWork>();

        for (int i = 0; i < _facesBase.Count; i++) {
            GameObject face = _facesBase[i];
            Vector3 start = face.transform.position;
            Vector3 target = buildIcon.transform.position;
            Rect rect = buildIcon.GetComponent<RectTransform>().rect;

            AnimateWork moveToBuild = new AnimateCurveMoveToPosition( face.transform, new Vector3[2] { start, target}, 30 );
            moveToBuild.setCombineWork(
                    new AnimateRectTo(face.GetComponent<RectTransform>(), rect, 30)
                ).setWaitBefore(i*8);
            moveToBuild.setWaitBefore(i * 8).setNextWork(
                    new AnimateFadeOut(face.transform, 20)
                ).setEnd( () => {
                    face.SetActive(false); }
                ).setCombineWork(
                    new AnimateRectTo(face.GetComponent<RectTransform>(), new Vector3(2, 2, 1), 30)
                );

            animates.Add( moveToBuild );
        }
        return animates.ToArray();
    }
    public AnimateWorker getBaseBackFadeAnimate() {
        return new AnimateWorker(
                new AnimateFadeOut( _baseBack.transform, 20)
            ).setEnd( () => {
                clearBase(); }
            );
    }

    public void update() { }
}