using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Game : Singleton<UI_Game>
{
    private Dictionary<UIID, string> UINames = new Dictionary<UIID, string>() {
        {UIID.BlockRaycast, "Canvas - BlockRaycast" },
        {UIID.MainMenu, "Canvas - MainMenu" },
        {UIID.Counting, "Canvas - Counting" },
        {UIID.ScoreBoard, "Canvas - ScoreBoard" },
        {UIID.Skin, "Canvas - Skin" },
        {UIID.LeaderBoard, "Canvas - LeaderBoard" },
        {UIID.IAP, "Canvas - IAP" },
        {UIID.GamePlay, "Canvas - GamePlay" },
    };   


    private Dictionary<UIID, UICanvas> UICanvas = new Dictionary<UIID, UICanvas>();

    public Transform CanvasParentTF;

    #region Canvas

    public bool IsOpenedUI(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null && UICanvas[ID].gameObject.activeInHierarchy;
    }

    public UICanvas GetUI(UIID ID)
    {
        if (!UICanvas.ContainsKey(ID) || UICanvas[ID] == null)
        {
            UICanvas canvas = Instantiate(Resources.Load<UICanvas>("UI/" + UINames[ID]), CanvasParentTF);
            UICanvas[ID] = canvas;
        }

        return UICanvas[ID];
    }

    public UICanvas OpenUI(UIID ID)
    {
        UICanvas canvas = GetUI(ID);

        canvas.Setup();
        canvas.Open();

        return canvas;
    }

    public bool IsOpened(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null;
    }

    #endregion

    #region Back Button

    private Dictionary<UICanvas, UnityAction> BackActionEvents = new Dictionary<UICanvas, UnityAction>();
    private List<UICanvas> backCanvas = new List<UICanvas>();
    UICanvas BackTopUI {
        get
        {
            UICanvas canvas = null;
            if (backCanvas.Count > 0)
            {
                canvas = backCanvas[backCanvas.Count - 1];
            }

            return canvas;
        }
    }


    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape) && BackTopUI != null)
        {
            BackActionEvents[BackTopUI]?.Invoke();
        }
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!BackActionEvents.ContainsKey(canvas))
        {
            BackActionEvents.Add(canvas, action);
        }
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!backCanvas.Contains(canvas))
        {
            backCanvas.Add(canvas);
        }
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        backCanvas.Remove(canvas);
    }

    /// <summary>
    /// CLear backey when comeback index UI canvas
    /// </summary>
    public void ClearBackKey()
    {
        backCanvas.Clear();
    }

    #endregion

    public void CloseUI(UIID ID)
    {
        if (IsOpened(ID))
        {
            GetUI(ID).Close();
        }
    }

}
