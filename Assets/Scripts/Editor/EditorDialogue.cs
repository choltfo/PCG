using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorDialogue : EditorWindow {
	DialogueTree dia;
	EditorWindow asdf;

	float scrollX;
	float scrollY;


	[MenuItem ("CGP/Window/Dialogue Editor")]
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(EditorDialogue));
	}

	/// <summary>
	/// Creates a rectangle inside of another rectangle.
	/// </summary>
	/// <returns>A rectangle at coords x,y -> x+width,y+height inside of rel.</returns>
	/// <param name="rel">The relatice rectangle.</param>
	/// <param name="x">The local x coordinate of the new rectangle.</param>
	/// <param name="y">The local y coordinate of the new rectangle..</param>
	/// <param name="width">The width of the new rectangle.</param>
	/// <param name="height">The height of the new rectangle.</param>
	/// <param name="mode">Defaultss to floating.</param>
	Rect relRec (Rect rel, float x, float y, float width, float height, relRecMode mode = relRecMode.floating) {
		if (mode == relRecMode.floating)
			return new Rect (rel.x+x,rel.y+y,width,height);
		else if (mode == relRecMode.singleEdgeClamp)
			// Clamps RHS edge to within rel.
			return new Rect (rel.x+x,rel.y+y,
			                 (x + width) > rel.width ? rel.width - x : width,
			                 (y + height) > rel.height ? rel.height - y : height);
		else /*clamp*/
			return new Rect (rel.x+x < 0 ? 0 : rel.x+x,
			                 rel.y+y < 0 ? 0 : rel.y+y,
			                 (x + width) > rel.width ? rel.width - x : width,
			                 (y + height) > rel.height ? rel.height - y : height);
	}


	void OnGUI () {
		EditorGUI.LabelField (new Rect(0,0,position.width,20),"Dialogue Editor");
		dia = (DialogueTree)EditorGUI.ObjectField (new Rect(0,15,200,15),"", dia, typeof(DialogueTree));

		Rect DiaBox = new Rect (5, 35, position.width - 10, position.height - 40);

		GUI.Box (relRec(DiaBox, 0,0,DiaBox.width,DiaBox.height), "");
	}

	void drawNode() {

	}
}

enum relRecMode {
	clamp,			/// Clamps the rectangle to within the parent.
	floating,		/// Allows the rectangle to leave the bounds of the parent.
	singleEdgeClamp	/// Clamps the right and botttom eedges to within the parent.
}


