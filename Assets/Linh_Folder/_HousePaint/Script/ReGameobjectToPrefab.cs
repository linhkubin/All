using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
public static class ReGameobjectToPrefab
{
    [MenuItem("Auto/ReGameobjectToPrefab")]
    public static void FindAndReGameobjectToPrefabInSelected()
    {
        #region create prefab source

        Dictionary<string, GameObject> dictPrefabs = new Dictionary<string, GameObject>();
        GameObject[] prefabSource = Resources.LoadAll<GameObject>("Furniture");

        dictPrefabs.Clear();

        for (int i = 0; i < prefabSource.Length; i++)
        {
            dictPrefabs.Add(prefabSource[i].name, prefabSource[i]);
        }

        #endregion 

        // EditorUtility.CollectDeepHierarchy does not include inactive children
        var deeperSelection = Selection.gameObjects.SelectMany(go => go.GetComponentsInChildren<Transform>(true))
            .Select(t => t.gameObject);

        var prefabs = new HashSet<Object>();
        int goCount = 0;

        //destroy list
        List<GameObject> destroys = new List<GameObject>();

        foreach (var go in deeperSelection)
        {
            string name = go.name;
            if (go.name.Contains(" "))
            {
                int lastIndex = name.IndexOf(" ");
                name = name.Remove(lastIndex);
            }
            //Debug.Log(name);
            //create prefab
            if (dictPrefabs.ContainsKey(name))
            {
                GameObject newPrefab = PrefabUtility.InstantiatePrefab(dictPrefabs[name], go.transform.parent) as GameObject;

                newPrefab.transform.localPosition = go.transform.localPosition;
                newPrefab.transform.localRotation = go.transform.localRotation;
                newPrefab.transform.localScale = go.transform.localScale;

                goCount++;

                destroys.Add(go.gameObject);
            }

        }

        //destroy old object
        while (destroys.Count > 0)
        {
            Object.DestroyImmediate(destroys[0]);
            destroys.RemoveAt(0);
        }

        Undo.RegisterCompleteObjectUndo(Selection.activeGameObject, "");

        Debug.Log($"Found and change {goCount} GameObjects");
    }

}