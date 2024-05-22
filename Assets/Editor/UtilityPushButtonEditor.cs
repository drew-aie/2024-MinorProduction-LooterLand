using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(UtilityPushButton))]
public class UtilityPushButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //show the normal inspector.
        base.OnInspectorGUI();

        //must get the target class, cast it. (special casting method.)
        UtilityPushButton pushButton = target as UtilityPushButton;

        //add a new button called "Invoke" and if it is pressed
        if(GUILayout.Button("Invoke"))
        {
            //invoke the event of UtilityPushEditor.
            pushButton.Event.Invoke();
        }
    }
}
