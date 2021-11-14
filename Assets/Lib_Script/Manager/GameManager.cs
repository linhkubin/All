using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] UserData userData;
    //[SerializeField] CSVData csv;
    public static float DeltaTime;
    private GameState gameState = GameState.MainMenu;

    /// <summary>
    /// khi update tien can phai update them cai gi day
    /// </summary>
    public UnityAction OnChangeCashEvent;

    public GameState State => gameState;

    // Start is called before the first frame update
    void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.captureDeltaTime = 0.02f;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //csv.OnInit();
        userData?.OnInitData();
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
