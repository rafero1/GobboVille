using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BehaviourAgent : MonoBehaviour {
    // Atributos
    public int hunger;
    public int hungerThreshold = 100;
    public bool isHungry {
        get {
            if (hunger >= hungerThreshold)
                return true;
            else return false;
        }
    }
    public int thirst;
    public int thirstThreshold = 100;
    public bool isThirsty {
        get {
            if (thirst >= thirstThreshold)
                return true;
            else return false;
        }
    }

    public void InitAttributes () {
        hunger = Random.Range (0, hungerThreshold);
        thirst = Random.Range (0, thirstThreshold);
    }

    // Pathfinding
    public Transform Target;
    public Vector3 targetPosition;

}