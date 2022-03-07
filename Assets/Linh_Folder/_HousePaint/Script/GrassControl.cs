using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HousePaint
{
    public class GrassControl : GameUnit
    {
        public Transform housePlat;
        public void OnInit(Vector2Int size)
        {
            List<Wall> plats = PrefabManager.Instance.GetNewPlats(size);

            housePlat.localPosition = Vector3.zero;
            housePlat.localPosition = new Vector3(size.x, 1, size.y);

            for (int i = 0; i < plats.Count; i++)
            {
                Wall wall = Instantiate(plats[i], new Vector3(size.x * ((i + 1) % 2), 0, size.y * (-(i + 1) / 2)) , Quaternion.identity, tf);
            }

        }
    }
}