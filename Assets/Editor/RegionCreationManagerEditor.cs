using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

[CustomEditor(typeof(Region))]
public class RegionCreationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //setup default inspector
        base.OnInspectorGUI();

        //get a reference to the region we are altering.
        Region region = (Region)target;

        if(GUILayout.Button("Create new spawn area"))
        {
            //if there is no container to store our new area, return.
            if (region.RegionData == null)
                return;

            //get a path to the prefab we want to create
            string prefabPath = "Assets/_Project/Gameplay/Collectables/SpawnRegions/spawnArea_collectable.prefab";

            //load prefab to use
            GameObject obj = 
                (GameObject)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));

            //instantiate the prefab
            GameObject newArea = (GameObject)PrefabUtility.InstantiatePrefab(obj);

            //add it to our list of areas
            region.RegionData.SpawnAreas.Add(newArea);

            //set its parent to be the region object that it is a part of
            newArea.transform.SetParent(region.gameObject.transform);

            //add the creation of this object to the undo registry.
            Undo.RegisterCreatedObjectUndo(newArea, "Created a new spawn area.");

            //change this object to be what is selected in the hierarchy
            Selection.activeObject = newArea;
        }
    }
}
