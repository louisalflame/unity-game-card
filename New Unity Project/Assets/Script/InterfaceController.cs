using UnityEngine;
using System.Collections;

public class InterfaceController {
    public InterfaceController() {
        GameObject next = MonoBehaviour.Instantiate(Resources.Load("NextBtn")) as GameObject;
    }
}
