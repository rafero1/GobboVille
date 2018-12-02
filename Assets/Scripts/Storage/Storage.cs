using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour {
    // Capacidade máxima
    private int capacity = 15;

    // Nível atual
    private int units;

    public virtual int getUnits () {
        return units;
    }

    public virtual void setUnits (int amount) {
        units = amount;
    }

    public virtual void addUnit () {
        units++;
    }

    public virtual void addUnits (int amount) {
        units += amount;
    }

    public virtual void decreaseUnit () {
        units--;
    }

    public virtual void decreaseUnits (int amount) {
        units -= amount;
    }

    public virtual int getCapacity () {
        return capacity;
    }

    public virtual void setCapacity (int value) {
        capacity = value;
    }

    public virtual bool isFull () {
        if (units >= capacity)
            return true;
        else return false;
    }
}