using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HousePaint
{
    public class PrefabManager : Singleton<PrefabManager>
    {
        public Vector2Int[] list;

        private Dictionary<Vector2Int, Wall[]> walls = new Dictionary<Vector2Int, Wall[]>();
        private Dictionary<Vector2Int, Wall[]> filled = new Dictionary<Vector2Int, Wall[]>();

        // Start is called before the first frame update
        void Awake()
        {
            for (int i = 0; i < 4; i++)
            {
                walls.Add(list[i], Resources.LoadAll<Wall>("walls/" + list[i].x + "x" + list[i].y + "/empty"));
                filled.Add(list[i], Resources.LoadAll<Wall>("walls/" + list[i].x + "x" + list[i].y + "/filled"));
            }

        }

        public List<Wall> GetNewHouse(int isGame, Vector3Int size)
        {
            //tao list gia tri la tuong hay gach
            List<bool> listIsGame = new List<bool>();
            for (int i = 0; i < 4; i++)
            {
                listIsGame.Add(i < isGame);
            }

            //mat dau tien phai la isGame
            do
            {
                listIsGame = Enums.SortOrder<bool>(listIsGame);
            } 
            while (!listIsGame[0]);

            //tao list tuong
            List<Wall> walls = new List<Wall>();

            Vector2Int FB = new Vector2Int(size.y, size.x);
            Vector2Int LR = new Vector2Int(size.y, size.z);

            walls.Add(listIsGame[0] ? GetWall(FB) : GetFilled(FB));
            walls.Add(listIsGame[1] ? GetWall(LR) : GetFilled(LR));
            walls.Add(listIsGame[2] ? GetWall(FB) : GetFilled(FB));
            walls.Add(listIsGame[3] ? GetWall(LR) : GetFilled(LR));

            return walls;
        }

        private Wall GetWall(Vector2Int size)
        {
            Debug.Log("- " + size);
            //TODO: ve sau sex check xem co bi lap lai k
            return walls[size][Random.Range(0, walls[size].Length)];
        }

        private Wall GetFilled(Vector2Int size)
        {
            Debug.Log("+ " + size);
            return filled[size][Random.Range(0, filled[size].Length)];
        }
    }
}