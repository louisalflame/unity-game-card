using UnityEngine;
using System.Collections;

public class GameLoopControl : MonoBehaviour {

    SceneController sceneController = new SceneController();

    void awake(){
        // 切換場景不會被刪除
        GameObject.DontDestroyOnLoad( this.gameObject );
    }

	void Start () {
        // 設定起始scene
        sceneController.setScene(new StartScene(sceneController));
	}
	
	// 所有更新的起點
	void Update () {
        // 由SceneController負責場景下的更新
        sceneController.sceneUpdate();
	}


}
