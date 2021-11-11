using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintRoller : Singleton<PaintRoller>
{
    public Transform tf;
    public MeshRenderer child;
    private Wall wall;
    public Vector2Int index;

    bool active;

    Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Left);
                target = wall.GetPoint(index);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Right);
                target = wall.GetPoint(index);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Up);
                target = wall.GetPoint(index);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Down);
                target = wall.GetPoint(index);
            }
        }

        tf.position = Vector3.MoveTowards(tf.position, target, Time.deltaTime * 15f);
    }

    public void SetActive(bool active)
    {
        this.active = active;
        child.material = HousePaint.Level.Instance.newPaint;
        child.gameObject.SetActive(active);
    }

    public void SetSide(Side side)
    {
        //TODO: xoay
    }

    public void SetWall(Wall wall)
    {
        this.wall = wall;
    }

    public void SetIndex(Vector2Int index, Vector3 position)
    {
        this.index = index;
        tf.position = position;
        target = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        WallUnit wallUnit = other.GetComponent<WallUnit>();
        if (wallUnit != null)
        {
            //this.index = wallUnit.index;
            wallUnit.Contact();
        }
    }
}
