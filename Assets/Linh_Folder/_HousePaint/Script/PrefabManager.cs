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
            for (int i = 0; i < list.Length; i++)
            {
                walls.Add(list[i], Resources.LoadAll<Wall>("walls/" + list[i].x + "x" + list[i].y + "/empty"));
                filled.Add(list[i], Resources.LoadAll<Wall>("walls/" + list[i].x + "x" + list[i].y + "/filled"));
            }

        }

        public List<Wall> GetNewHouse(int isGameplay, Vector3Int size)
        {
            //tao list gia tri la tuong hay gach
            List<bool> listIsGame = new List<bool>();
            for (int i = 0; i < 4; i++)
            {
                listIsGame.Add(i < isGameplay);
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

        public List<Wall> GetNewPlats(Vector2Int size)
        {
            //tao list plat
            List<Wall> plats = new List<Wall>();

            for (int i = 0; i < 3; i++)
            {
                plats.Add(GetPlat(size));
            }

            return plats;
        }

        private Wall GetWall(Vector2Int size)
        {
            //Debug.Log("- " + size);
            //TODO: ve sau sexcheck xem co bi lap lai k
            //Debug.Log(size);
            return walls[size][Random.Range(0, walls[size].Length)];
        }
        
        public Wall GetPlat(Vector2Int size)
        {
            //Debug.Log("- " + size);
            //TODO: ve sau sexcheck xem co bi lap lai k
            //Debug.Log(size);
            return walls[size][Random.Range(0, walls[size].Length)];
        }

        private Wall GetFilled(Vector2Int size)
        {
            //Debug.Log(size);
            return filled[size][Random.Range(0, filled[size].Length)];
        }

        public bool IsHaveHouseSize(Vector2Int size, ref int height)
        {
            bool isHaveHouse = false;
            List<int> heights = new List<int>();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].y == size.x)
                {
                    for (int j = i + 1; j < list.Length; j++)
                    {
                        if (list[j].y == size.y)
                        {
                            if (list[i].x == list[j].x)
                            {
                                heights.Add(list[i].x);
                                isHaveHouse = true;
                                break;
                            }
                        }
                    }
                }

            }

            if (isHaveHouse)
            {
                height = heights[Random.Range(0, heights.Count)];
            }

            return isHaveHouse;
        }
    }
}