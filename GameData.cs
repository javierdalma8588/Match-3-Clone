using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] highscores;
    public int[] stars; 
}

public class GameData : MonoBehaviour
{
    public SaveData saveData;
    public static GameData gameData;

    void Awake()
    {
        if(gameData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Load(); 
    }

    public void Save()
    {
        //Create a binary formatter that can read binary files
        BinaryFormatter formatter = new BinaryFormatter();

        //Create a route from the program to the file
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);

        //Create a copy of save data
        SaveData data = new SaveData();
        data = saveData;

        //save the data in the file
        formatter.Serialize(file, data);

        //close the data stream
        file.Close();
        Debug.Log("Saved");
    }

    public void Load()
    {
        //Check if the save game file exists
        if (File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            //Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Loaded");
        }
        else
        {
            saveData = new SaveData();
            saveData.isActive = new bool[100];
            saveData.stars = new int[100];
            saveData.highscores = new int[100];
            saveData.isActive[0] = true;
            
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
