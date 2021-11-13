
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
public static class ReGameobjectToPrefab
{
    [MenuItem("Auto/ReGameobjectToPrefab")]
    private static void FindAndReGameobjectToPrefabInSelected()
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

        Debug.Log($"Found and change {goCount} GameObjects");
    }

    // Prefabs can both be nested or variants, so best way to clean all is to go through them all
    // rather than jumping straight to the original prefab source.
    private static void RecursivePrefabSource(GameObject instance, HashSet<Object> prefabs, ref int compCount,
        ref int goCount)
    {
        var source = PrefabUtility.GetCorrespondingObjectFromSource(instance);
        // Only visit if source is valid, and hasn't been visited before
        if (source == null || !prefabs.Add(source))
            return;

        // go deep before removing, to differantiate local overrides from missing in source
        RecursivePrefabSource(source, prefabs, ref compCount, ref goCount);

        int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(source);
        if (count > 0)
        {
            Undo.RegisterCompleteObjectUndo(source, "Remove missing scripts");
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(source);
            compCount += count;
            goCount++;
        }
    }
}