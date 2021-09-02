using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MakeTowerRoomScriptableObject 
{
    [MenuItem("Assets/Create/Tower Room limit")]
    public static void CreateAsset()
    {

        TowerLimitTemplate asset = ScriptableObject.CreateInstance<TowerLimitTemplate>();

        AssetDatabase.CreateAsset(asset, "Assets/Tower_Room_Info.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    
    }
}
