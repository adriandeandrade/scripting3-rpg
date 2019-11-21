using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;
using enjoii.Items;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Player player;
    public Player PlayerRef
    {
        get
        {
            if (player == null) player = FindObjectOfType<Player>();
            return player;
        }
    }

    public ItemDatabase ItemDatabase { get; private set; }
    public RecipeDatabase RecipeDatabase { get; private set; }
    public SceneController SceneController { get; private set; }

    public void Initialize()
    {
        Debug.Log("Game Manager Initialized.");
        SaveSystem.Init();

        RecipeDatabase = FindObjectOfType<RecipeDatabase>();
        ItemDatabase = FindObjectOfType<ItemDatabase>();
        SceneController = FindObjectOfType<SceneController>();
        player = FindObjectOfType<Player>();
    }

    private void InitSingleton()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        InitSingleton();

        RecipeDatabase = FindObjectOfType<RecipeDatabase>();
        ItemDatabase = FindObjectOfType<ItemDatabase>();
        SceneController = FindObjectOfType<SceneController>();
        player = FindObjectOfType<Player>();
    }

    public void Save()
    {
        player.Save();
    }

    public void Load()
    {
        player.Load();
    }
}
