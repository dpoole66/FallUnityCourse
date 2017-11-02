using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PositionSaver : MonoBehaviour {
    // Things to save:
    public Vector3 LastPosition = Vector3.zero;
    public Quaternion LastRotation = Quaternion.identity;
    private Transform ThisTransform = null;

	// Use this for initialization
	void Awake () {
        ThisTransform = GetComponent<Transform>();
	}
    void Start() {
        LoadObject();
    }

    // Save JSON
    void SaveObject () {
        // Output path and data
        string OutputPath = Application.persistentDataPath + @"\ObjectPosition.json";
        LastPosition = ThisTransform.position;
        LastRotation = ThisTransform.rotation;

        // Create Writer object
        StreamWriter Writer = new StreamWriter(OutputPath);
        Writer.WriteLine(JsonUtility.ToJson(this));
        Writer.Close();
        Debug.Log("JSON Path " + OutputPath);
	}

    // Load JSON object
    void LoadObject() {
        string InputPath = Application.persistentDataPath + @"\ObjectPosition.json";
        StreamReader Reader = new StreamReader(InputPath);
        string JSONString = Reader.ReadToEnd();
        Debug.Log("Reading JSON String " + JSONString);
        JsonUtility.FromJsonOverwrite(JSONString, this);
        Reader.Close();

        ThisTransform.position = LastPosition;
        ThisTransform.rotation = LastRotation;
    }

    void OnDestroy() {
        SaveObject();
    }
}
