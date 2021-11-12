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
    public BoxCollider collider;

    public Animator anim;
    private MoveSlide slide;

    bool active;
    bool isMoving;

    Vector3 target;

    bool appear;

    // Update is called once per frame
    void Update()
    {
        if (active && isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Left);
                target = wall.GetPoint(index);
                slide = MoveSlide.Left;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Right);
                target = wall.GetPoint(index);
                slide = MoveSlide.Right;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Up);
                target = wall.GetPoint(index);
                slide = MoveSlide.Up;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                index = wall.SlidePoint(index, MoveSlide.Down);
                target = wall.GetPoint(index);
                slide = MoveSlide.Down;
            }
        }

        tf.position = Vector3.MoveTowards(tf.position, target, Time.deltaTime * 30f);
        isMoving = (tf.position - target).sqrMagnitude < 0.1f;
    }

    public void SetActive(bool active)
    {
        collider.enabled = active;
        this.active = active;
        isMoving = active;
        child.gameObject.SetActive(active);
        //child.material = HousePaint.Level.Instance.newPaint;
    }

    public void SetWall(Wall wall)
    {
        this.wall = wall;
        appear = true ;
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

            if (wallUnit.index == index && !appear)
            {
                anim.SetTrigger(slide.ToString());
            }
            appear = false;
        }
    }
}
