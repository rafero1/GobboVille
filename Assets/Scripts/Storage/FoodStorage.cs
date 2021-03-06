﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : Storage {
    public TextMesh displayText;

    // Use this for initialization
    void Start () {
        setUnits (getCapacity ());
        displayText = gameObject.GetComponentInChildren<TextMesh> ();
        displayText.text = $"{getUnits()}/{getCapacity()}";
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log($"Nível de comida: {foodUnits}", this);
    }

    override public void onUnitChangeListener (int oldValue, int newValue) {
        displayText.text = $"{getUnits()}/{getCapacity()}";
    }
}