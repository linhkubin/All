using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(Sorting))]
public class SortingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        if (GUILayout.Button("Sorting"))
        {
            Sorting sorting = (Sorting)target;
            sorting.Sort();
        }
        
        if (GUILayout.Button("ChangeMat"))
        {
            Sorting sorting = (Sorting)target;
            sorting.ChangeMat();
        }
    }
}

#endif
