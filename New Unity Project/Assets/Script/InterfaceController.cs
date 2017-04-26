using UnityEngine;
using System.Collections;

public class InterfaceController {
    public InterfaceController() {
        GameObject next = MonoBehaviour.Instantiate(Resources.Load("NextBtn")) as GameObject;

        GameObject exit = MonoBehaviour.Instantiate(Resources.Load("ExitBtn")) as GameObject;
        exit.transform.localPosition = new Vector3(-3, 4, 1);
        exit.transform.localScale = new Vector3(1, 1, 1);
    }
}
