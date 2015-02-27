using UnityEngine;
using System.Collections;

[System.Serializable]
public class DialogueNode {
//TODO: Note http://forum.unity3d.com/threads/access-variable-by-string-name.42487/. Use for parameters on outlinks.

public class DialogueNode : ScriptableObject {
	/// Simple name for the node, since a long line is hard to show in editor.
	public string name = "DATA";

	/// The actual text to show as a subtitle or something.
	public string text = "Hello!";
	/// The sound clip to play while this node is being run.
	public AudioClip sound;
	
	public int nextNode;

	/// The array of options. Resizable in editor.
	public DialogueOutlink[] options;

	/// Is this needed? I really don't know....
	public float lengthOfTime;
	
	/// <summary>
	/// Editor only!
	/// </summary>
	public Rect nodePos;
}
