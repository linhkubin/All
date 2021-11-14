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
    public ParticleSystem tail;

    public Animator anim;
    private MoveSlide slide;

    bool active;
    bool isMoving;

    Vector3 target;

    bool appear;

    Vector3 lastPoint;

    // Update is called once per frame
    void Update()
    {
        if (active && isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPoint = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector2 distance = Input.mousePosition - lastPoint;
                MoveSlide moveSlide = MoveSlide.None;

                if (distance.sqrMagnitude > 300)
                {
                    if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
                    {
                        moveSlide = (distance.x > 0) ? MoveSlide.Right : MoveSlide.Left;
                    }
                    else
                    {
                        moveSlide = (distance.y > 0) ? MoveSlide.Up : MoveSlide.Down;
                    }
                }

                SetTarget(index, moveSlide);

                lastPoint = Input.mousePosition;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //index = wall.SlidePoint(index, MoveSlide.Left);
                //target = wall.GetPoint(index);
                //slide = MoveSlide.Left;

                SetTarget(index, MoveSlide.Left);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //index = wall.SlidePoint(index, MoveSlide.Right);
                //target = wall.GetPoint(index);
                //slide = MoveSlide.Right;

                SetTarget(index, MoveSlide.Right);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //index = wall.SlidePoint(index, MoveSlide.Up);
                //target = wall.GetPoint(index);
                //slide = MoveSlide.Up;

                SetTarget(index, MoveSlide.Up);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //index = wall.SlidePoint(index, MoveSlide.Down);
                //target = wall.GetPoint(index);
                //slide = MoveSlide.Down;

                SetTarget(index, MoveSlide.Down);
            }
        }

        tf.position = Vector3.MoveTowards(tf.position, target, Time.deltaTime * 30f);
        isMoving = (tf.position - target).sqrMagnitude < 0.1f;
    }

    private void SetTarget(Vector2Int index, MoveSlide moveSlide)
    {
        if (moveSlide != MoveSlide.None)
        {
            this.index = wall.SlidePoint(index, moveSlide);
            target = wall.GetPoint(this.index);
            slide = moveSlide;
        }
    }

    public void SetActive(bool active)
    {
        collider.enabled = active;
        this.active = active;
        isMoving = active;
        tail.Clear();
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
