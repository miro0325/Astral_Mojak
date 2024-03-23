using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Camera Cam
    {
        get
        {
            return cam;
        }
    }

    public Player CurPlayer
    {
        get
        {
            return curPlayer;
        }
    }

    
    [SerializeField] private Camera cam;
    [SerializeField] private List<Player> playerList = new();

    private Player curPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
