using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : Storage {
    // Use this for initialization
    void Start () {
        setUnits(Random.Range (0, getCapacity()));
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log($"Nível de comida: {foodUnits}", this);
    }

}