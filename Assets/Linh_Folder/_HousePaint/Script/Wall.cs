using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveSlide { Left, Right, Up, Down}

public class Wall : GameUnit
{
    [HideInInspector]public House house;
    public Transform child;
    public PainterStartPos startPoint;
    public WallUnit unitPrefab;

    [HideInInspector] public Dictionary<Vector2Int, WallUnit> unitsRemain = new Dictionary<Vector2Int, WallUnit>();



    [HideInInspector] public Dictionary<Vector2Int, WallUnit> units = new Dictionary<Vector2Int, WallUnit>();

    public Vector2Int Index => startPoint.index;
    public Vector3 StartPoint => GetPoint(Index);
    public Side Side { get; internal set; }

    Vector2Int Size;

    private void OnValidate()
    {
        //TODO: need commet when build
        child = tf.Find("Board");
        startPoint = tf.Find("Environment").Find("PainterStartPos").GetComponent<PainterStartPos>();
    }

    // Start is called before the first frame update
    public void OnInit()
    {
        OnReset();

        Vector3 size = child.localScale;

        Size.x = (int)size.x;
        Size.y = (int)size.z;

        Vector3 startPoint = Vector3.forward * (size.z / 2 - 0.5f) + Vector3.left * (size.x / 2 - 0.5f);

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                WallUnit unit = Instantiate(unitPrefab, tf);
                unit.tf.localPosition = startPoint + Vector3.right * i + Vector3.back * j;
                unit.index = new Vector2Int(i, j);
                unit.wall = this;
                unit.SetMat(HousePaint.Level.Instance.originPaint);
                unitsRemain.Add(new Vector2Int(i, j), unit);
                units.Add(new Vector2Int(i, j), unit);
            }
        }
    }

    internal void IsObstacle(WallUnit wallUnit)
    {
        units.Remove(wallUnit.index);
        unitsRemain.Remove(wallUnit.index);
        wallUnit.name = "aaaaaaaaaaaaaaa";
        Debug.Log("isObstacle");
    }

    public Vector3 GetPoint(Vector2Int index)
    {
        return units[index].tf.position;
    }

    private void OnReset()
    {
        foreach (var unit in unitsRemain)
        {
            Destroy(unit.Value.gameObject);
        }

        unitsRemain.Clear();
    }

    public void Paint(WallUnit unit)
    {
        if (unitsRemain.Count > 0)
        {
            unit.Painted();

            unitsRemain.Remove(unit.index);
            // check xem het chua
            if (unitsRemain.Count <= 0)
            {
                //Win or thong bao cho house next
                house.NextWall();
            }
        }
    }


    public Vector2Int SlidePoint(Vector2Int index, MoveSlide slide)
    {
        Vector2Int start = index;
        Vector2Int finish = index;

        switch (slide)
        {
            case MoveSlide.Left:
                for (int i = 1; i < Size.x; i++)
                {
                    finish.x = start.x - i;
                    if (!units.ContainsKey(finish) || units[finish] == null)
                    {
                        finish.x++;
                        break;
                    }
                }
                break;

            case MoveSlide.Right:
                for (int i = 1; i < Size.x; i++)
                {
                    finish.x = start.x + i;
                    if (!units.ContainsKey(finish) || units[finish] == null)
                    {
                        finish.x--;
                        break;
                    }
                }
                break;

            case MoveSlide.Up:
                for (int i = 1; i < Size.y; i++)
                {
                    finish.y = start.y - i;

                    if (!units.ContainsKey(finish) || units[finish] == null)
                    {
                        finish.y++;
                        break;
                    }
                }
                break;

            case MoveSlide.Down:
                for (int i = 1; i < Size.y; i++)
                {
                    finish.y = start.y + i;
                    if (!units.ContainsKey(finish) || units[finish] == null)
                    {
                        finish.y--;
                        break;
                    }
                }
                break;
        }

        return finish;
    }

}
