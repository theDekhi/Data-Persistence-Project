using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    //Transits between scenes, contains the player name which has been entered in entry, contain best score in both scenes...
    public static GameManager gameManager;
    private Button startButton;
    private InputField nameInput;
    public static int bestScore = 0;
    public static string bestPlayerName = "";
    public string playerName;
    public Text bestScoreText;
    private void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = this;
        DontDestroyOnLoad(gameObject);
        LoadGame();
    }
    private void Start()
    {
        bestScoreText.text = $"Best Score : {bestPlayerName} : {bestScore}";
        nameInput = GameObject.Find("InputField").GetComponent<InputField>();
        startButton = GameObject.Find("Start Button").GetComponent<Button>();
        nameInput.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    public void ValueChangeCheck()
    {
        startButton.enabled = nameInput.text.Length != 0 && !nameInput.text.Contains(" ");
    }


    public void OnStartButtonPressed()
    {
        playerName = nameInput.text;
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonPressed()
    {
        //Quits app...
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void OnLeaderboardButtonPressed()
    {
        SceneManager.LoadScene(2);
    }

    [System.Serializable]
    public class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }
    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = playerName;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);

    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = new SaveData();
            data = JsonUtility.FromJson<SaveData>(json);
            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
            bestScoreText.text = $"Best Score : {bestPlayerName} : {bestScore}";
        }

    }
}
