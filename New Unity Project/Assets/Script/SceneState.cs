using UnityEngine;
using System.Collections;

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
}

public class StartScene : SceneState{
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
}

public class MenuScene : SceneState {
    public MenuScene(SceneController controller)
        : base(controller)
    {
        Debug.Log("new menuScene");
        stateName = "menu";
        loadScene = true;
    }

    public override void stateBegin() { 
        Debug.Log("menuScene begin:");
        GameObject exit = MonoBehaviour.Instantiate(Resources.Load("ExitBtn")) as GameObject;
        GameObject start = MonoBehaviour.Instantiate(Resources.Load("StartBtn")) as GameObject; 
    }
    public override void stateUpdate() {  }
    public override void stateEnd() { }
}
