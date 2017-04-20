using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneState{
    protected SceneController sceneController;
    protected string stateName = "AbstractSceneState";
    protected bool loadScene = false;
    protected bool readyBegin = false;

    public SceneState(SceneController controller) {
        sceneController = controller;
    }
    public string getStateName() { return stateName; }
    public bool isNeedToLoad() { return loadScene; }
    public bool isReadyBegin() { return readyBegin; }
    public void readyToBegin(bool ready) { readyBegin = ready; }

    public virtual void stateBegin() { }
    public virtual void stateEnd() { }
    public virtual void stateUpdate() { }

    //玩家輸入
    public virtual void inputProcess(){}
}

public class StartScene : SceneState {
    public StartScene(SceneController controller)
        : base(controller)
    {
        Debug.Log("new StartScene");
        stateName = "start";
    }

    public override void stateBegin() { 
        /*進行需要資源的初始載入*/
        Debug.Log("startScene begin: load menuscene");
        sceneController.setScene(new MenuScene(sceneController));
    }
    public override void stateUpdate() { }
    public override void stateEnd() { Debug.Log("startScene end:"); }

    public override void inputProcess() {
        InputController.Inputs.resetQueue();
    }
}

public class MenuScene : SceneState {
    public GameObject exit;
    public GameObject start;

    public MenuScene(SceneController controller)
        : base(controller)
    {
        Debug.Log("new menuScene");
        stateName = "menu";
        loadScene = true;
    }

    public override void stateBegin() { 
        Debug.Log("menuScene begin:");
        exit = MonoBehaviour.Instantiate(Resources.Load("ExitBtn")) as GameObject;
        start = MonoBehaviour.Instantiate(Resources.Load("StartBtn")) as GameObject; 
    }
    public override void stateUpdate() {  }
    public override void stateEnd() { }

    //玩家輸入
    public override void inputProcess() {
        Queue<string> inputs = InputController.Inputs.getInputsQueue();
        InputController.Inputs.resetQueue();
        foreach (string i in inputs)
        {
            Debug.Log("input process : " + i);
            //按下start鈕=>開啟新scene
            if (i == "start_battle") {
                sceneController.setScene(new BattleScene(sceneController));
            }
        }
    }
}

public class BattleScene : SceneState {
    //戰鬥流程總管理 1.程式邏輯 2.UI圖面
    private BattleController battleController;
    
    public BattleScene(SceneController controller)
        : base(controller)
    {
        Debug.Log("new battleScene");
        stateName = "battle";
        loadScene = true;
    }

    public override void stateBegin() {
        Debug.Log("battleScene begin:");
        battleController = new BattleController();
    }
    public override void stateUpdate() { }
    public override void stateEnd() { }

    public override void inputProcess() {
        Queue<string> inputs = InputController.Inputs.getInputsQueue();
        InputController.Inputs.resetQueue();
        foreach (string i in inputs)
        {
            Debug.Log("input process : " + i);
            //按下next鈕=>下一個turn階段
            if (i == "next_turn") {
                battleController.CountResultAndNextTurn();
            }
        }
    }
}
