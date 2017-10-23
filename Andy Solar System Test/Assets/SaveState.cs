using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveState  {

    private const string FILENAME = "saveData.json";
    private string filePath = Path.Combine(Application.streamingAssetsPath, FILENAME);
    public Planets.SceneTypes scene;

    public int var = 2;

    public void loadData() {
		if(File.Exists(filePath))
		{
			string dataAsJson = File.ReadAllText(filePath);

			SaveState[] planets = JsonHelper.FromJson<SaveState>(dataAsJson);

			Debug.Log("Save File loaded.");
		}
		else
		{
			Debug.LogError("Cannot load Saved data!");
		}
    }

    public void saveData() {
        Debug.Log("Saving.");
        string dataAsJson = JsonUtility.ToJson (this);
        File.WriteAllText (filePath, dataAsJson);
        Debug.Log("Saved: " + filePath + " : " + dataAsJson);
    }
}
