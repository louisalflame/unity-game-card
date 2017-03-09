using UnityEngine;
using System.Collections;

public class SceneController {
     
    private SceneState state;

    public SceneController() { }

    public void setScene( SceneState next ){
        //state.stateEnd();
        state = next;
        state.stateBegin();
    }
	
	// 場景下所有的更新
    public void sceneUpdate() {
        state.stateUpdate();
    }

    // 圖片的移動
    public void FixedUpdate() {
    }

    //玩家輸入
    public void InputProcess() {
    }
}
