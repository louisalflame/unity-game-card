using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceInterface {
    public InterfaceController _interface;
    public GameObject _charPlace { get; private set; }
    public GameObject _character { get; private set; }
    public GameObject _bottomLine { get; private set; }
    public GameObject _posture { get; private set; }

    public PlayerPlaceInterface(InterfaceController inter) {
        _interface = inter;
    }
    public void create() {
        create_BattleScene_PlayerBattlePlace(_interface.getImageLeftBattleField());
    }
    public void create_BattleScene_PlayerBattlePlace(GameObject parent) {
        _charPlace = CanvasFactory.createEmptyRect(parent, "PlayerBattlePlace");
        CanvasFactory.setRectTransformAnchor(_charPlace, new Vector2(0f, 0.37f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        _character = CanvasFactory.createImage(_charPlace, "Character");
        CanvasFactory.setImageSprite(_character, "Sprite/CharPlace/tmpChar");
        CanvasFactory.setRectTransformPosition(_character, new Vector2(0.7f, 0f), new Vector2(0.7f, 0f), Vector2.zero, Vector2.zero);
        CanvasFactory.setRectPivot(_character, new Vector2(0.5f, 0f));
        CanvasFactory.setImageNatureSize(_character);

        _bottomLine = CanvasFactory.createImage(_charPlace, "BottomLine");
        CanvasFactory.setImageSprite(_bottomLine, "Sprite/CharPlace/bottomLine");
        CanvasFactory.setRectTransformAnchor(_bottomLine, new Vector2(0f, 0f), new Vector2(1f, 0.05f), Vector2.zero, Vector2.zero);

        _posture = CanvasFactory.createImage(_charPlace, "Posture");
        CanvasFactory.setImageSprite(_posture, "Sprite/CharPlace/posture");
        CanvasFactory.setRectTransformPosition(_posture, new Vector2(1f, 0.1f), new Vector2(1f, 0.1f), Vector2.zero, new Vector2(120f, 30f));
        CanvasFactory.setRectPivot(_posture, new Vector2(1f, 0f));

        _charPlace.SetActive(false);
    }
    public void initial() {
        _character.GetComponent<RectTransform>().anchoredPosition = new Vector2(800f, 0f);
        _bottomLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500f, 0f);
        _posture.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500f, 0f);
        _charPlace.SetActive(true);
    }
}

public class EnemyPlaceInterface {
    public InterfaceController _interface;
    public GameObject _charPlace { get; private set; }
    public GameObject _character { get; private set; }
    public GameObject _bottomLine { get; private set; }
    public GameObject _posture { get; private set; }

    public EnemyPlaceInterface(InterfaceController inter) {
        _interface = inter;
    }
    public void create() {
        create_BattleScene_EnemyBattlePlace(_interface.getImageRightBattleField());
    }
    public void create_BattleScene_EnemyBattlePlace(GameObject parent) {
        _charPlace = CanvasFactory.createEmptyRect(parent, "EnemyBattlePlace");
        CanvasFactory.setRectTransformAnchor(_charPlace, new Vector2(0f, 0.47f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        _character = CanvasFactory.createImage(_charPlace, "Character");
        CanvasFactory.setImageSprite(_character, "Sprite/CharPlace/tmpChar");
        CanvasFactory.setRectTransformPosition(_character, new Vector2(0.3f, 0f), new Vector2(0.3f, 0f), Vector2.zero, Vector2.zero);
        CanvasFactory.setRectPivot(_character, new Vector2(0.5f, 0f));
        CanvasFactory.setImageNatureSize(_character);

        _bottomLine = CanvasFactory.createImage(_charPlace, "BottomLine");
        CanvasFactory.setImageSprite(_bottomLine, "Sprite/CharPlace/bottomLine");
        CanvasFactory.setRectTransformAnchor(_bottomLine, new Vector2(0f, 0f), new Vector2(1f, 0.05f), Vector2.zero, Vector2.zero);

        _posture = CanvasFactory.createImage(_charPlace, "Posture");
        CanvasFactory.setImageSprite(_posture, "Sprite/CharPlace/posture");
        CanvasFactory.setRectTransformPosition(_posture, new Vector2(0f, 0.1f), new Vector2(0f, 0.1f), Vector2.zero, new Vector2(120f, 30f));
        CanvasFactory.setRectPivot(_posture, new Vector2(0f, 0f));

        _charPlace.SetActive(false);     
    }
    public void initial() {
        _character.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800f, 0f);
        _bottomLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(500f, 0f);
        _posture.GetComponent<RectTransform>().anchoredPosition = new Vector2(500f, 0f);
        _charPlace.SetActive(true);
    }
}