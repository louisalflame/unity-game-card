using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour {

    public float BaseWidth = 1024f;
    public float BaseHeight = 576f;
    public float BaseOrthographicSize = 9f;

    public void Awake() {
        /* 自適應綁定攝影機長比例的方法
        GameObject.DontDestroyOnLoad(this.gameObject);

        float newOrthographicSize = (float)Screen.height / (float)Screen.width * BaseWidth / BaseHeight * BaseOrthographicSize;

        Camera _camera = this.gameObject.GetComponent<Camera>();
        _camera.orthographicSize = Mathf.Max( newOrthographicSize, BaseOrthographicSize );
        */
    }
}
