using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;
using enjoii.Items;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Player player;

    public ItemDatabase ItemDatabase { get; private set; }
    public RecipeDatabase RecipeDatabase { get; private set; }
    public SaveManager SaveManager { get; private set; }

    public Player PlayerRef
    {
        get
        {
            if (player == null) player = FindObjectOfType<Player>();
            return player;
        }
    }

    public void Initialize()
    {
        Debug.Log("Game Manager Initialized.");
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
        SaveManager = FindObjectOfType<SaveManager>();
        player = FindObjectOfType<Player>();
    }
}
