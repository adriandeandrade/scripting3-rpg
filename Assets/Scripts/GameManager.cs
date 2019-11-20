using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using enjoii.Characters;
using enjoii.Items;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Player player;
    private ItemDatabase itemDatabase;

//    public static GameManager Instance
//    {
//        get
//        {
//            if (instance == null) instance = Instantiate(Resources.Load<GameManager>("GameManager"));
//            return instance;
//;        }
//    }

    public ItemDatabase ItemDatabase
    {
        get
        {
            return itemDatabase;
        }
    }

    public Player PlayerRef
    {
        get
        {
            if (player == null) player = FindObjectOfType<Player>();
            return player;
        }
    }
    

    private void InitSingleton()
    {
        //Check if instance already exists
        if (Instance == null)

            //if not, set instance to this
            Instance = this;

        //If instance already exists and it's not this:
        else if (Instance != this)

            //Then destroy this. This enforces our singleton pattern, 
            // meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene / Switching scenes
        DontDestroyOnLoad(gameObject); // VERY IMPORTANT
    }

    private void Awake()
    {
        InitSingleton();
        itemDatabase = FindObjectOfType<ItemDatabase>();
        player = FindObjectOfType<Player>();
    }
}
