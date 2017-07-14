using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneState{
    protected SceneController sceneController;
    protected string stateName = "AbstractSceneState";
    protected bool loadScene = false;
    protected bool readyBegin = false;
    protected bool loadComplete = false;

    public SceneState(SceneController controller) {
        sceneController = controller;
    }
    public string getStateName() { return stateName; }
    public bool isNeedToLoad() { return loadScene; }
    public bool isReadyBegin() { return readyBegin; }
    public void readyToBegin(bool ready) { readyBegin = ready; }
    public bool isLoadComplete() { return loadComplete; }
    public void loadToComplete(bool complete) { loadComplete = complete; }

    public virtual void stateBegin() { }
    public virtual void stateEnd() { }
    public virtual void stateUpdate() { }

    //玩家輸入
    public virtual void inputProcess(){}
}

public class StartScene : SceneState {
    public StartScene(SceneController controller) : base(controller) {
        Debug.Log("new StartScene");
        stateName = "start";
    }

    public override void stateBegin() { 
        /*進行需要資源的初始載入*/
        Debug.Log("startScene begin: load menuscene");
        sceneController.setScene(new MenuScene(sceneController));
    }
    public override void stateUpdate() { }
    public override void stateEnd() { Debug.Log("startScene end"); }

    public override void inputProcess() {
        InputController.Inputs.resetQueue();
    }
}



public class BattleScene : SceneState {
    //戰鬥流程總管理 1.程式邏輯 2.UI圖面
    private BattleController battleController;
    
    public BattleScene(SceneController controller) : base(controller) {
        Debug.Log("new battleScene");
        stateName = "battle";
        loadScene = true;
    }

    public override void stateBegin() {
        Debug.Log("battleScene begin:");
        battleController = new BattleController(sceneController);
    }
    public override void stateUpdate() {
        battleController.update();
    }
    public override void stateEnd() { }

    public override void inputProcess() {
        //某階段(動畫中) 停止玩家指令
        if (!battleController.isInputValid()) { return; }
        Queue<string> inputs = InputController.Inputs.getInputsQueue();
        InputController.Inputs.resetQueue();

        foreach (string input in inputs) {
            Debug.Log("input process : " + input);
            //按下start鈕=>開啟新scene
            if (input == NameCoder.getLabel(NameCoder.ExitButton)) {
                sceneController.setScene(new MenuScene(sceneController));
            }else {
                battleController.inputProcess(input);
            }
        }

    }
}
