using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceInterface {
    public InterfaceController _interface;
    public GameObject _charPlace { get; private set; }

    public PlayerPlaceInterface(InterfaceController inter) {
        _interface = inter;

        _charPlace = CanvasFactory.create_BattleScene_PlayerBattlePlace(_interface.getImageLeftBattleField());
    }
}

public class EnemyPlaceInterface {
    public InterfaceController _interface;
    public GameObject _charPlace { get; private set; }

    public EnemyPlaceInterface(InterfaceController inter) {
        _interface = inter;

        _charPlace = CanvasFactory.create_BattleScene_EnemyBattlePlace(_interface.getImageRightBattleField());
    }
}