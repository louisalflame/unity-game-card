using UnityEngine;
using System.Collections;

public class SceneState{
    protected SceneController sceneController;
    protected string stateName = "AbstractSceneState";

    public SceneState(SceneController controller) {
        sceneController = controller;
    }
    public string getStateName() { return stateName;  }

    public virtual void stateBegin() { }
    public virtual void stateEnd() { }
    public virtual void stateUpdate() { }
}

public class StartScene : SceneState{
    public StartScene(SceneController controller)
        : base(controller)
    {
        Debug.Log("StartScene");
        stateName = "StartScene";
    }

    public override void stateBegin() { /*進行需要資源的初始載入*/ }
    public override void stateUpdate() {
        Debug.Log("start load menuscene");
        sceneController.setScene(new MenuScene(sceneController));
    }
    public override void stateEnd() { }
}

public class MenuScene : SceneState {
    public MenuScene(SceneController controller)
        : base(controller)
    {
        Debug.Log("menuscene");
        stateName = "MenuScene";
    }

    public override void stateBegin() { /*進行需要資源的初始載入*/
        Application.LoadLevel("menu");
    }
    public override void stateUpdate() {
        Debug.Log("menuscene updating");
    }
    public override void stateEnd() { }
}
