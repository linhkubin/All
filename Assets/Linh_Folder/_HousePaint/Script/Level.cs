using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HousePaint
{
    public class Level : Singleton<Level>
    {
        public Material originPaint;
        public Material newPaint;

        public House house;

        public void Start()
        {
            //TODO: setup level
            UI_Game.Instance.OpenUI(UIID.MainMenu);
            //size house
            house.OnInit(2, new Vector3Int(4, 4, 5));
        }
    }
}
