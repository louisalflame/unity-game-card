using UnityEngine;
using System.Collections;

public class GameDiceManager : MonoBehaviour {

    public enum Process
    {
        prepare,
        mov_prepare_dice,
        mov_pop_dice,
        mov_throw_dice,
        mov_arrange,
        mov_collect_build,
        mov_skill,
        atk_arrange,
        def_arrange,
        atk_skill,
        def_skill,        
        turn_conclusion
    }

    public Process process;

	// Use this for initialization
	void Start () {
        process = Process.prepare;
	}
	
	// Update is called once per frame
	void Update () {
        switch (process)
        {
            case Process.prepare:
                Debug.Log("prepare");
                process = Process.mov_prepare_dice;
                break;
            case Process.mov_prepare_dice:
                break;
        }
	}
}
