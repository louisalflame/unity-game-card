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

public class MenuScene : SceneState {

    public MenuScene(SceneController controller) : base(controller) {
        Debug.Log("new menuScene");
        stateName = "menu";
        loadScene = true;
    }

    public override void stateBegin() { 
        Debug.Log("menuScene begin:");

        GameObject start = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        start.transform.localPosition = new Vector3(0,2,1);
        start.transform.localScale=new Vector3(3,3,1);
        start.transform.Find("text").GetComponent<TextMesh>().text = "START";
        start.GetComponent<Button>().ButtonID = "start_battle";

        GameObject exit = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject; 
        exit.transform.localPosition = new Vector3(0,-2,1);
        exit.transform.localScale=new Vector3(3,3,1);
        exit.transform.Find("text").GetComponent<TextMesh>().text = "EXIT";
        
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
    
    public BattleScene(SceneController controller) : base(controller) {
        Debug.Log("new battleScene");
        stateName = "battle";
        loadScene = true;
    }

    public override void stateBegin() {
        Debug.Log("battleScene begin:");
        battleController = new BattleController();
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

        foreach (string i in inputs) {
            Debug.Log("input process : " + i);
            //按下next鈕=>下一個turn階段
            if (i == "next_turn") {
                battleController.CountResultAndNextTurn();
            }
            else if (i == "exit") {
                sceneController.setScene(new MenuScene(sceneController));
            }
            else if (i == "throw_dice") {
                battleController.throwDices();
            }
            else if (StringCoder.isBelongAttrDecision(i) ) {
                battleController.decisionAttr(StringCoder.getDecisionNum(i));
            }
            else if (StringCoder.isBelongBaseDecision(i) ) {
                battleController.decisionBase(StringCoder.getDecisionNum(i));
            }
            else if (i == Move_GetFirst.label) {
                battleController.moveAction(Move_GetFirst.moveAction);
            }
            else if (i == Move_Exchange.label) {
                battleController.moveAction(Move_Exchange.moveAction);
            }
            else if (i == Move_Standby.label) {
                battleController.moveAction(Move_Standby.moveAction);
            }
            else if (StringCoder.isBelongChangeChar(i)) {
                battleController.changeActiveChar( StringCoder.getChangeCharNum(i) );
            }
        }
    }
}
