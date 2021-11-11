using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Front, Back, Left, Right }

public class House : GameUnit
{
    public Wall[] wallPrefab;
    private List<Wall> walls = new List<Wall>();
    private Vector2 size;
    int index;

    // Start is called before the first frame update
    public void OnInit(Vector2Int size)
    {
        World.Instance.OnInit();

        //need follow side
        InitWall(wallPrefab[0], size, Side.Front);
        InitWall(wallPrefab[1], size, Side.Right);
        InitWall(wallPrefab[2], size, Side.Back);
        InitWall(wallPrefab[3], size, Side.Left);

        //random 2-3 wall
        //random con lai trong list k paint
        index = 0;

        StartWall(index);
    }

    public void InitWall(Wall newWall, Vector2 size, Side side)
    {
        Wall wall = Instantiate(newWall, tf);
        wall.house = this;
        walls.Add(wall);

        Vector3 point = Vector3.zero;
        Vector3 face = Vector3.zero;

        switch (side)
        {
            case Side.Front:
                point = new Vector3(0, size.y / 2, -size.x / 2);
                face = new Vector3(-90, 0, 0);
                break;

            case Side.Back:
                point = new Vector3(0, size.y / 2, size.x / 2);
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
        wall.OnInit();
    }

    public void NextWall()
    {
        //TODO: xoay lai theo goc camera
        PaintRoller.Instance.SetActive(false);

        index++;

        if (index <= 3)
        {
            World.Instance.SetTarget(Vector3.up * index * 90, ()=> StartWall(index));
            Debug.Log("index "+ index);
        }
        else
        {
            World.Instance.SetWin(HousePaint.Level.Instance.Start);
        }

    }

    private void StartWall(int index)
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
            PaintRoller.Instance.SetSide(wall.Side);
            PaintRoller.Instance.SetActive(true);
        }
    }
}
