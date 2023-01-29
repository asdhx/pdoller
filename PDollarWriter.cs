using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class PDollarWriter : MonoBehaviour
{


    private List<Gesture> trainingSet = new List<Gesture>();

    private List<Point> points;
    private int strokeId;

    private Vector3 virtualKeyPosition;
    private Rect drawArea;

    private RuntimePlatform platform;


    private int deltaFrames;

    public string newGestureName;


    void Start()
    {
        //Set initial values for render input
        deltaFrames = 0;
        strokeId = -1;
        points = new List<Point>();



        drawArea = new Rect(0, 0, Screen.width, Screen.height);
        virtualKeyPosition = Vector2.zero;

        platform = Application.platform;

        //Load pre-made gestures
    }

    void Update()
    {
        //check wheter we are on phone or desktop and notice cursor position
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        }
        else
        {
            if (drawArea.Contains(virtualKeyPosition))
            {


                if (Input.GetMouseButtonDown(0) && deltaFrames == 0)
                {
                    strokeId++;
                    deltaFrames++;
                }
                if (Input.GetMouseButton(0) && strokeId > -1)
                {
                    virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
                    updatePoints();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    deltaFrames = 0;

                }
                if (Input.GetKeyDown("w"))
                    addGesture();

            }
        }

    }


    void cleanUp()
    {
        points.Clear();
        strokeId = -1;
    }


    void updatePoints()
    {
        points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));
    }

    void addGesture()
    {
        //string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName, DateTime.Now.ToFileTime());
        string fileName = String.Format("{0}.xml", newGestureName);
        GestureIO.WriteGesture(points.ToArray(), newGestureName, fileName);
        //trainingSet.Add(new Gesture(points.ToArray(), newGestureName));
        newGestureName = "default";
        cleanUp();
    }

}
