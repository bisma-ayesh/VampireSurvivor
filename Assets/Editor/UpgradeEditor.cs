using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UpgradeEditor : EditorWindow

{
    [MenuItem("Tools/UpgradeEditor")]

    public static void StartWondow()
    {
        GetWindow<UpgradeEditor>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Hellow");
        if (GUILayout.Button("Bruh"))
        {
            Debug.Log("Cool tool");
        }
    }
}
