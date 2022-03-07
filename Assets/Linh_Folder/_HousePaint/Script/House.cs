using HousePaint;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Front, Back, Left, Right }

public class House : GameUnit
{
    public Transform roof;
    public Transform plat;
    private List<Wall> walls = new List<Wall>();
    private Vector2 size;
    int index;

    /// <summary>
    /// house size
    /// </summary>
    /// <param name="size"></param>
    public void OnInitHouse(int isGame, Vector3Int size)
    {
        World.Instance.OnInit();
        tf.localScale = Vector3.one;
        tf.rotation = Quaternion.identity;
        tf.position = Vector3.zero;

        //stage la so man choi game
        //need follow side
        Debug.Log(size);

        DestroyHouse();


        List<Wall> walls = PrefabManager.Instance.GetNewHouse(Random.Range(2, 4), size);

        InitWall(walls[0], size, Side.Front);
        InitWall(walls[1], size, Side.Right);
        InitWall(walls[2], size, Side.Back);
        InitWall(walls[3], size, Side.Left);

        plat.localScale = Vector3.right * (size.x + 1) + Vector3.forward * (size.z + 1) + Vector3.up * 0.3f;
        roof.position = Vector3.up * size.y;
        roof.localScale = Vector3.right * (size.z + 2) / 2 + Vector3.forward * (size.x + 2) / 2 + Vector3.up;

        tf.localScale = Vector3.one * 0.7f;
        tf.rotation = Quaternion.Euler(Vector3.up * 90);
        tf.position = Vector3.up * 0.1f;
    }

    // x = truc x, y = truc y, z = truc z
    public void InitWall(Wall newWall, Vector3 size, Side side)
    {
        Wall wall = Instantiate(newWall, tf);
        wall.house = this;
        walls.Add(wall);

        Vector3 point = Vector3.zero;
        Vector3 face = Vector3.zero;

        switch (side)
        {
            case Side.Front:
                point = new Vector3(0, size.y / 2, -size.z / 2);
                face = new Vector3(-90, 0, 0);
                break;

            case Side.Back:
                point = new Vector3(0, size.y / 2, size.z / 2);
                face = new Vector3(-90, 180, 0);
                break;

            case Side.Left:
                point = new Vector3(-size.x / 2, size.y / 2, 0);
                face = new Vector3(-90, 90, 0);
                break;

            case Side.Right:
                point = new Vector3(size.x / 2, size.y / 2, 0);
                face = new Vector3(-90, -90, 0);
                break;
        }


        wall.tf.SetPositionAndRotation(point, Quaternion.Euler(face));
        wall.Side = side;
        //wall.OnInit();
    }

    public void OnInitPlats(Vector2Int size)
    {
        List<Wall> plats = PrefabManager.Instance.GetNewPlats(size);

        //housePlat.localPosition = Vector3.zero;
        //housePlat.localPosition = new Vector3(size.x, 1, size.y);

        for (int i = 0; i < plats.Count; i++)
        {
            Wall wall = Instantiate(plats[i], new Vector3(size.y * ((i + 1) % 2), 0, size.x * (-(i + 1) / 2)), Quaternion.identity);
            wall.OnInit();
            wall.house = this;
            walls.Add(wall);
        }

        index = 0;
    }


    public void NextWall()
    {
        PaintRoller.Instance.SetActive(false);

        index++;

        if (index <= 3)
        {
            World.Instance.SetTarget(Vector3.up * index * 90, ()=> StartWall(index));
        }
        else
        {
            World.Instance.SetWin(HousePaint.Level.Instance.Start);
        }

    }

    public void StartWall(int index = 0)
    {
        Wall wall = walls[index];

        if (wall.IsEmpty)
        {
            NextWall();
        }
        else
        {

            PaintRoller.Instance.SetWall(wall);
            PaintRoller.Instance.SetIndex(wall.Index, wall.StartPoint);
            //PaintRoller.Instance.SetSide(wall.Side);
            PaintRoller.Instance.SetActive(true);
        }
    }

    private void DestroyHouse()
    {
        //TODO: destroy all

        //while (walls.Count > 0)
        //{
        //    Destroy(walls[0].gameObject);
        //    walls.RemoveAt(0);
        //}
    }
}
