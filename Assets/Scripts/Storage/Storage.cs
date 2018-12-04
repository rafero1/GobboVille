using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Storage : MonoBehaviour {
    // Capacidade máxima
    public int capacity;

    // Nível atual
    public int units;

    public virtual int getUnits () {
        return units;
    }

    public virtual void setUnits (int amount) {
        int oldValue = units;
        units = amount;
        onUnitChangeListener (oldValue, units);
    }

    public virtual void addUnit () {
        int oldValue = units;
        units++;
        onUnitChangeListener (oldValue, units);
    }

    public virtual void addUnits (int amount) {
        int oldValue = units;
        units += amount;
        onUnitChangeListener (oldValue, units);
    }

    public virtual void decreaseUnit () {
        int oldValue = units;
        units--;
        onUnitChangeListener (oldValue, units);
    }

    public virtual void decreaseUnits (int amount) {
        int oldValue = units;
        units -= amount;
        onUnitChangeListener (oldValue, units);
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

    public virtual bool isEmpty () {
        if (units <= 0)
            return true;
        else return false;
    }

    public virtual void onUnitChangeListener (int oldValue, int newValue) { }
}