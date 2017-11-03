using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public static bool movingToScene = false;

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/game.info");

        SceneData data = new SceneData();
        data.planetRadius = CreateSystems.planetSizeChange;
        data.orbitRadius = CreateSystems.orbitSizeChange;
        data.yearMulti = CreateSystems.yearMultiplier;
        data.scene = SceneManager.GetActiveScene().name;
        data.page = CreateSystems.goToPage;

        bf.Serialize(file, data);
        file.Close();
    }
    public static void Load()
    {
        if (File.Exists(Application.dataPath + "/game.info"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/game.info", FileMode.Open);
            SceneData data = (SceneData)bf.Deserialize(file);
            file.Close();
            if (SceneManager.GetActiveScene().name == data.scene && movingToScene)
            {
                movingToScene = false;
                CreateSystems.planetSizeChange = data.planetRadius;
                CreateSystems.orbitSizeChange = data.orbitRadius;
                CreateSystems.yearMultiplier = data.yearMulti;
                CreateSystems.goToPage = data.page;
                return;
            }
            else
            {
                movingToScene = true;
                SceneManager.LoadScene(data.scene);
            }
        }
    }
    /// <summary>
    /// called when moving from 2D to 3D by clicking on a planet
    /// </summary>
    /// <param name="planetName"> the name of the planet clicked</param>
    /// <param name="page">the int number of the page the planet is from </param>
    public static void MoveTo3DScene()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/game.info");

        SceneData data = new SceneData();
        data.planetRadius = CreateSystems.planetSizeChange;
        data.orbitRadius = CreateSystems.orbitSizeChange;
        data.yearMulti = CreateSystems.yearMultiplier;
        data.scene = "ThreeDSystems";
        data.page = CreateSystems.goToPage;

        bf.Serialize(file, data);
        file.Close();
        SceneManager.LoadScene(data.scene);
    }
}

class SceneData
{
    public float planetRadius = 1;
    public float orbitRadius = 1;
    public float yearMulti = 1;
    public string scene = null;
    public int page = -1;
}