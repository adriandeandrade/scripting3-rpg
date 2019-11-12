using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enjoii.Characters;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Player player;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = Instantiate(Resources.Load<GameManager>("GameManager"));
            return instance;
;        }
    }

    public Player PlayerRef
    {
        get
        {
            if (player == null) player = FindObjectOfType<Player>();
            return player;
        }
    }
    

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
}
