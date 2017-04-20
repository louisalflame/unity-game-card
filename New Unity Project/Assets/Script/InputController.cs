using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

public class InputController {

    private static InputController _inputs;
    public static InputController Inputs {
        get {
            if (_inputs == null) {
                _inputs = new InputController();
            }
            return _inputs;
        }
    }

    private Queue<string> inputsQueue ;

    private InputController() {
        resetQueue();
    }

    public void addInput(string input) {
        inputsQueue.Enqueue(input);
    }

    public Queue<string> getInputsQueue() {
        return inputsQueue;
    }

    public void resetQueue() {
        inputsQueue = new Queue<string>();
    }
}
