using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : SceneState {

    private GameObject _canvas;

    public MenuScene(SceneController controller) : base(controller) {
        Debug.Log("new menuScene");
        stateName = "menu";
        loadScene = true;
    }

    public override void stateBegin() { 
        Debug.Log("menuScene begin:");

        _canvas = CanvasFactory.createCanvas();
        GameObject _panel = CanvasFactory.createBasicRatioPanel(_canvas);
        GameObject _startBtn = CanvasFactory.create_MenuScene_StartBtn(_panel);
        
        // 隊伍設定按鈕
        
    }
    public override void stateUpdate() {  }
    public override void stateEnd() {
        GameObject.Destroy(_canvas);
    }

    //玩家輸入
    public override void inputProcess() {
        Queue<string> inputs = InputController.Inputs.getInputsQueue();
        InputController.Inputs.resetQueue();
        foreach (string i in inputs)
        {
            Debug.Log("input process : " + i);
            //按下start鈕=>開啟新scene
            if (i == NameCoder.getLabel(NameCoder.StartButton)) {
                sceneController.setScene(new BattleScene(sceneController));
            }
        }
    }


}