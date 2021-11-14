using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HousePaint
{
    public class Level : Singleton<Level>
    {
        public Material[] materials;
        public Material originPaint;
        [HideInInspector] public Material newPaint;

        public House house;

        [Header("----- Camera -----")]
        public AnimationCurve curve;

        public void Start()
        {
            newPaint = materials[Random.Range(0, materials.Length)];

            //setup level
            UI_Game.Instance.OpenUI(UIID.MainMenu);
            //size house
            Vector3Int size = GetHouseSize();
            house.OnInit(2, size);

            CameraControl.Instance.SetPoint(Vector3.up * size.y / 2 + Vector3.back * curve.Evaluate(Mathf.Max(size.x, size.z)), Quaternion.identity);
        }

        private Vector3Int GetHouseSize()
        {
            //TODO: setup theo level
            int y = Random.Range(4, 13);
            Vector2Int[] list = PrefabManager.Instance.list;
            List<Vector2Int> listFollowY = new List<Vector2Int>();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].x == y)
                {
                    listFollowY.Add(list[i]);
                }
            }

            listFollowY = Enums.SortOrder(listFollowY);

            return Vector3Int.right * listFollowY[0].y + Vector3Int.up * y + Vector3Int.forward * listFollowY[1].y;
        }
    }
}
