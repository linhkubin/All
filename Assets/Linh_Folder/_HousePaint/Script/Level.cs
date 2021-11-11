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
            house.OnInit(new Vector2Int(4,4));
        }
    }
}
