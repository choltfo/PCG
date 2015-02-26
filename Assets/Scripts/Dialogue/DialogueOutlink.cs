using UnityEngine;
using System.Collections;

/// <summary>
/// An outgoing option on a node.
/// </summary>
public class DialogueOutlink {
	public bool endDialogue = false;
	public DialogueType type;
	public string title = "Yes?/Okay?/Continue.../What?/Tulsa./Thatway";
	public DialogueNode to;
}