using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 行動選單按鈕
public class ActionButtonInterface { 
    public InterfaceController _interface;

    public GameObject[] _movActions { get; private set; }
    public GameObject[] _atkActions { get; private set; }
    public GameObject[] _defActions { get; private set; }

    public ActionButtonInterface(InterfaceController inter) {
        _interface = inter;

        _movActions = new GameObject[] { };
        _atkActions = new GameObject[] { };
        _defActions = new GameObject[] { };
    }

    public void update() { }


    public void setMoveActionButton(CharManager character) {
        _movActions = new GameObject[ character._movActions.Length ];
        for (int i = 0; i < _movActions.Length; i++) {
            GameObject actBtn = createMovActionButton( character._movActions[i].getLabel_ID() ); 
            actBtn.SetActive(false);
            _movActions[i] = actBtn;
        } 
    }
    public void showMoveActionButton() {
        foreach (GameObject btn in _movActions) { btn.SetActive(true); }
    }
    public void hideMoveActionButton() {
        foreach (GameObject btn in _movActions) { btn.SetActive(false); }
    }

    public void setAttackActionButton(CharManager character) { 
        _atkActions = new GameObject[ character._atkActions.Length ];
        for (int i = 0; i < _atkActions.Length; i++) {
            GameObject actBtn = createAtkActionButton( character._atkActions[i].getLabel_ID() );
            actBtn.SetActive(false);
            _atkActions[i] = actBtn;
        }
    }
    public void showAttackActionButton() {
        foreach (GameObject btn in _atkActions) { btn.SetActive(true); }
    }
    public void hideAttackActionButton() {
        foreach (GameObject btn in _atkActions) { btn.SetActive(false); }
    }

    public void setDefenseActionButton(CharManager character) { 
        _defActions = new GameObject[ character._defActions.Length ];
        for (int i = 0; i < _defActions.Length; i++) {
            GameObject actBtn = createDefActionButton( character._defActions[i].getLabel_ID() );
            actBtn.SetActive(false);
            _defActions[i] = actBtn;
        }
    }
    public void showDefenseActionButton() { 
        foreach (GameObject btn in _defActions) { btn.SetActive(true); }
    }
    public void hideDefenseActionButton() { 
        foreach (GameObject btn in _defActions) { btn.SetActive(false); }
    }

    public void cleanActionButtons() {
        foreach (GameObject btn in _movActions) { MonoBehaviour.Destroy(btn); }
        foreach (GameObject btn in _atkActions) { MonoBehaviour.Destroy(btn); }
        foreach (GameObject btn in _defActions) { MonoBehaviour.Destroy(btn); }
    }
    public void resetActionButtons(CharManager character) {
        cleanActionButtons();
        setMoveActionButton(character);
        setAttackActionButton(character);
        setDefenseActionButton(character);
    }

    private GameObject createMovActionButton(string[] label_ID) {
        GameObject actionBtn = CanvasFactory.createButton(_interface.getImageActionBase(), "actionBtn", NameCoder.getLabel(label_ID));
        CanvasFactory.setImageSprite(actionBtn, "Sprite/ActionButton/movActBack");

        CanvasFactory.setAspectRatioFitter(actionBtn, AspectRatioFitter.AspectMode.HeightControlsWidth, 2f / 3f);

        GameObject top = CanvasFactory.createImage(actionBtn, "top");
        CanvasFactory.setImageSprite(top, "Sprite/ActionButton/movActUp");
        CanvasFactory.setWholeRect(top);
        GameObject info = CanvasFactory.createImage(actionBtn, "info");
        CanvasFactory.setImageSprite(info, "Sprite/ActionButton/movActInfo");
        CanvasFactory.setWholeRect(info);
        GameObject frame = CanvasFactory.createImage(actionBtn, "side");
        CanvasFactory.setImageSprite(frame, "Sprite/ActionButton/movActFrame");
        CanvasFactory.setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, "txt", NameCoder.getText(label_ID));
        CanvasFactory.setRectTransformAnchor(txt, Vector2.zero, new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        CanvasFactory.setTextScaleSize(txt, 0.1f, 150);
        CanvasFactory.setTextColor(txt, Color.white);

        return actionBtn;
    }
    private GameObject createAtkActionButton(string[] label_ID) {
        GameObject actionBtn = CanvasFactory.createButton(_interface.getImageActionBase(), "actionBtn", NameCoder.getLabel(label_ID));
        CanvasFactory.setImageSprite(actionBtn, "Sprite/ActionButton/atkActBack");

        CanvasFactory.setAspectRatioFitter(actionBtn, AspectRatioFitter.AspectMode.HeightControlsWidth, 2f / 3f);

        GameObject top = CanvasFactory.createImage(actionBtn, "top");
        CanvasFactory.setImageSprite(top, "Sprite/ActionButton/atkActUp");
        CanvasFactory.setWholeRect(top);
        GameObject info = CanvasFactory.createImage(actionBtn, "info");
        CanvasFactory.setImageSprite(info, "Sprite/ActionButton/atkActInfo");
        CanvasFactory.setWholeRect(info);
        GameObject frame = CanvasFactory.createImage(actionBtn, "side");
        CanvasFactory.setImageSprite(frame, "Sprite/ActionButton/atkActFrame");
        CanvasFactory.setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, "txt", NameCoder.getText(label_ID));
        CanvasFactory.setRectTransformAnchor(txt, Vector2.zero, new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        CanvasFactory.setTextScaleSize(txt, 0.1f, 150);
        CanvasFactory.setTextColor(txt, Color.white);

        return actionBtn;
    }
    private GameObject createDefActionButton(string[] label_ID) {
        GameObject actionBtn = CanvasFactory.createButton(_interface.getImageActionBase(), "actionBtn", NameCoder.getLabel(label_ID));
        CanvasFactory.setImageSprite(actionBtn, "Sprite/ActionButton/defActBack");

        CanvasFactory.setAspectRatioFitter(actionBtn, AspectRatioFitter.AspectMode.HeightControlsWidth, 2f / 3f);

        GameObject top = CanvasFactory.createImage(actionBtn, "top");
        CanvasFactory.setImageSprite(top, "Sprite/ActionButton/defActUp");
        CanvasFactory.setWholeRect(top);
        GameObject info = CanvasFactory.createImage(actionBtn, "info");
        CanvasFactory.setImageSprite(info, "Sprite/ActionButton/defActInfo");
        CanvasFactory.setWholeRect(info);
        GameObject frame = CanvasFactory.createImage(actionBtn, "side");
        CanvasFactory.setImageSprite(frame, "Sprite/ActionButton/defActFrame");
        CanvasFactory.setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, "txt", NameCoder.getText(label_ID));
        CanvasFactory.setRectTransformAnchor(txt, Vector2.zero, new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        CanvasFactory.setTextScaleSize(txt, 0.1f, 150);
        CanvasFactory.setTextColor(txt, Color.white);

        return actionBtn;
    }
}