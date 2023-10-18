using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string bestPlayerName;
    public int bestScore;
    public string inputPlayerName;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {

    }

    public void ReadStringInput(string s)
    {
        inputPlayerName = s;
    }


    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int playerPoints;
    }

    public void SaveBestPlayer(string playerName, int playerScore)
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.playerPoints = playerScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.playerName;
            bestScore = data.playerPoints;
        }
    }
}
