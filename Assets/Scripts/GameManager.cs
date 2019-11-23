using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;
using enjoii.Items;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public Dialog startingDialog;

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

        RecipeDatabase = FindObjectOfType<RecipeDatabase>();
        ItemDatabase = FindObjectOfType<ItemDatabase>();
        SceneController = FindObjectOfType<SceneController>();
        player = FindObjectOfType<Player>();

        SaveSystem.Init();
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

    private void Start()
    {
        startingDialog = Resources.Load<Dialog>("Dialog/InitialDialogue");

        Debug.Log(startingDialog.instructions.Count);

        //DialogManager.Instance.StartDialog(startingDialog);
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
