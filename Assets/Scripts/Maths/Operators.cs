using UnityEngine;
using System.Collections;

public class Operators {

    public int Number
    {
        get { return Random.Range(0, 9); }
    }
    public string Add
    {
        get { return " + "; }
    }
    public string Subtract
    {
        get { return " - "; }
    }
    public string Divide
    {
        get { return " / "; }
    }
    public string Multiply
    {
        get { return " * "; }
    }
}
