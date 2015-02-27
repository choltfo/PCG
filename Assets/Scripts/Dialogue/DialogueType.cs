/// <summary>
/// The type of dialogue option that is presented.
/// </summary>
public enum DialogueType {
	Continue,	/// Continues with no options.
	Prompt,		/// Gives options. Use this for a prompted continue.
	Return,		/// Returns to root.
	Aassign		/// Assigns a variable in the tree.
}

