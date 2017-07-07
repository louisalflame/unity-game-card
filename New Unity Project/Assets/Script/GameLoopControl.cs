using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoopControl : MonoBehaviour {

    // 場景設定
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
        this.gameObject.transform.name = "GameLoop";

        sceneController = new SceneController(this);
        sceneController.setScene(new StartScene(sceneController));

        //調整重力方向，讓骰子能斜斜地站在平面上
        Physics.gravity = new Vector3(0, -30f, 30f);
	}
	
	// 所有更新的起點 => 由FixedUpdate取代
	void Update () { }
    void FixedUpdate() {
        // 由SceneController負責場景下的更新
        sceneController.sceneUpdate();
    }
    
}
