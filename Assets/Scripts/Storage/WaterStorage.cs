using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStorage : Storage {
    public TextMesh displayText;

    // Use this for initialization
    void Start () {
        setUnits (Random.Range (0, getCapacity ()));
        displayText = gameObject.GetComponentInChildren<TextMesh> ();
    }

    // Update is called once per frame
    void Update () {
        //Debug.Log($"Nível de água: {waterUnits}", this);
    }

    override public void onUnitChangeListener (int oldValue, int newValue) {
        displayText.text = $"{getUnits()}/{getCapacity()}";
    }

}