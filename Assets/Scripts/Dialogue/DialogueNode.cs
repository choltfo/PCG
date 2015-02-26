using UnityEngine;
using System.Collections;

public class DialogueNode : ScriptableObject {
	/// Simple name for the node, since a long line is hard to show in editor.
	public string name = "DATA";

	/// The actual text to show as a subtitle or something.
	public string text = "Hello!";
	/// The sound clip to play while this node is being run.
	public AudioClip sound;

	/// The array of options. Resizable in editor.
	public DialogueOutlink[] options;

	/// Is this needed? I really don't know....
	public float lengthOfTime;
}
