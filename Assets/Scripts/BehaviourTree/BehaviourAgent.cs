using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BehaviourAgent : MonoBehaviour {
    // Atributos
    public float hunger;
    public int hungerThreshold = 100;
    public float thirst;
    public int thirstThreshold = 100;

    public bool isHungry () {
        if (hunger >= hungerThreshold)
            return true;
        else return false;
    }
    public bool isThirsty () {
        if (thirst >= thirstThreshold)
            return true;
        else return false;
    }
    public void InitAttributes () {
        hunger = Random.Range (0, hungerThreshold);
        thirst = Random.Range (0, thirstThreshold);
    }
    public void updateAttributes () {
        hunger += (2 * Time.deltaTime);
        Debug.Log ($"hunger: {hunger}, thirst: {thirst}", this);
    }

    // Pathfinding
    public Transform Target;
    public Vector3 targetPosition;

}