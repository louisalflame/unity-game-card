using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController {
     
    private SceneState state = null;
    private GameLoopControl looper = null;

    public SceneController(GameLoopControl gameLoop) {
        looper = gameLoop;
    }

    //設置新場景 
    public void setScene( SceneState next ){
        //先將前一場景結束
        if( state != null ) { state.stateEnd(); }

        state = next;

        //如果場景需要實作loading 則以Async方式load新場景
        if( state.isNeedToLoad() ) {
            looper.StartCoroutine(sceneLoadAsync(state.getStateName()));
        } else { state.readyToBegin(true); }
    }

    //疊代器 : 不停地load微量新場景
    IEnumerator sceneLoadAsync(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
	    ao.allowSceneActivation = false;
 
	    while (! ao.isDone)
	    {
            // allowSceneActivation是false時，只會從0跑到90%
		    float progress = Mathf.Clamp01(ao.progress / 0.9f);
		    Debug.Log("Loading progress: " + (progress * 100) + "%");
 
		    // 當進度90%時表示load完成，可準備StateBegin()
		    if (ao.progress >= 0.9f)
		    {
			    ao.allowSceneActivation = true;
                state.readyToBegin(true);
                Debug.Log("ready to load 100%" );
		    }
 
		    yield return null;
	    }
    }
	
	// 場景下所有的更新
    public void sceneUpdate() {
        if ( state.isReadyBegin() ) {
            state.stateBegin();
            state.readyToBegin(false);
        }
        state.stateUpdate();  
    }

    //玩家輸入
    public void InputProcess() {
    }
}
