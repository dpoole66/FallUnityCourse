using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Functions : MonoBehaviour {

    //  i/o Display
    public Text iOText;
    private string ioResultDisplay;

    // Overload
    public int integer;
    public float[] arrayVals;

    public bool add;
    public bool subtract;
    public bool divide;

    public float valueA, valueB, result;

    private void Start() {
        DoSomething(ref integer);
        DoSomething(arrayVals);


     // Super Switcher

       result = Divide(valueA, valueB);
       print(result);
        if (add == true) {
           result = Sum(valueA, valueB);
        }

        if (subtract == true) {
            result = Subtract(valueA, valueB);
        }

        if (divide == true) {
            result = Divide(valueA, valueB);
        }

        IOUpdate();

    }

    public void SaySomething(string something) {
        Debug.Log(something);
    }

    void Update() {

    }

    // sub for return output

    void SubReturn() {
        result = Sum(valueA, valueB);
        print(result);
    }
    // Return types

    float Sum (float a, float b) {
        float value = a + b;
        return value;
    }

    float Subtract (float a, float b) {
        float result = a - b;
        return result;
    }

    float Divide(float a, float b) {
        if (b == 0) {
            Debug.Log("Cannot divide by Zed.");
            return 0;
        } else {
            float value = a / b;
            return value;
        }
            
    }

    // Overload functions passing by value and reference. 

    void DoSomething (ref int vee) {        // passing by Type
        vee += 3;
    }

    void DoSomething (float[] vee) {          // passing by Reference
        arrayVals[0] = 4.5f;
    }

    // IO Display
    void IOUpdate() {

        float ioresult = result;

        if (add == true) {
            ioResultDisplay = "The Sum is:  ";
        } else if (subtract == true) {
            ioResultDisplay = "The Result is:  ";
        } else if (divide == true){
            ioResultDisplay = "The Product is ";
        }

        iOText.text = ioResultDisplay + ioresult.ToString();
    }
 
}

