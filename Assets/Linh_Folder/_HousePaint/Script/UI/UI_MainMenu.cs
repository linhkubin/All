using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HousePaint
{
    public class UI_MainMenu : UICanvas
    {
        private House house;

        private void Start()
        {
            house = FindObjectOfType<House>();
        }

        public override void Open()
        {
            base.Open();
        }

        public override void Close()
        {
            base.Close();
        }

        public void TapToPlay()
        {
            Close();
            house.StartWall();
        }
    }
}
