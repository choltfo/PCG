﻿using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility {
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateAsset<T> () where T : ScriptableObject {
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");
		
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}

	/// <summary>
	/// Creates a dialogue tree instance in the current directory.
	/// </summary>
	[MenuItem("CGP/New.../Dialogue Tree")]
	public static void createDialogueTree() {
		CreateAsset <DialogueTree> ();
	}

	/// <summary>
	/// Creates a dialogue node instance in the current directory.
	/// </summary>
	/*[MenuItem("CGP/New.../Dialogue Node")]
	public static void createDialogueNode() {
		CreateAsset <DialogueNode> ();
	}*/
}

