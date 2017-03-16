using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoopControl : MonoBehaviour {

    SceneController sceneController = null;

    void awake(){
        // 切換場景不會被刪除
        Debug.Log("awake Mono");
        GameObject.DontDestroyOnLoad( this.gameObject );
    }

	void Start () {
        // 設定起始scene
        Debug.Log("Start Mono");
        GameObject.DontDestroyOnLoad(this.gameObject);

        sceneController = new SceneController(this);
        sceneController.setScene(new StartScene(sceneController));
	}
	
	// 所有更新的起點
	void Update () {
        // 由SceneController負責場景下的更新
        sceneController.sceneUpdate();
	}

    // 圖片的移動
    public void FixedUpdate() {
    }
    
}
