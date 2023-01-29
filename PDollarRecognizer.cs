using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using PDollarGestureRecognizer;

public class PDollarRecognizer : Singleton<PDollarRecognizer>
{
    public PDollarRecognizer()
    {

    }

    private List<Gesture> trainingSet = new List<Gesture>();

    void Start()
    {
        //Set initial values for render input


        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GameSet/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        //string[] filePaths = Directory.GetFiles("C:\\Users\\asd\\New Unity Project (3)\\Assets\\PDollar\\Resources\\GameSet", " *.xml");
        //foreach (string filePath in filePaths)
        //   trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
        //trainingSet.Add(GestureIO.ReadGestureFromFile("C:\\Users\\asd\\New Unity Project (3)\\lightning.xml"));
    }

    public String CallClassifier(List<Point> points)
    {
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
        Debug.Log(gestureResult.Score);
        Debug.Log(gestureResult.GestureClass);
        return gestureResult.GestureClass; 
    }

}
