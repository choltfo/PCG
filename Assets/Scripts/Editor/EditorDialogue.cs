using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorDialogue : EditorWindow {
	DialogueTree dia;

	float scrollX;
	float scrollY;

	int boxWidth = 300;
	int boxHeight = 50;
	
	Rect DiaBox;
	
	string[] names;


	[MenuItem ("CGP/Window/Dialogue Editor")]
	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(EditorDialogue));
	}
	
	public string[] getNames () {
		string[] output = new string[dia.nodes.Length];
		for (int i = 0; i < dia.nodes.Length; i ++) {
			output[i] = dia.nodes[i].name;
		}
		return output;
	}

	/// <summary>
	/// Creates a rectangle inside of another rectangle.
	/// </summary>
	/// <returns>A rectangle at coords x,y -> x+width,y+height inside of rel.</returns>
	/// <param name="rel">The relative rectangle.</param>
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
	
	Rect clampRect(Rect parent, Rect input) {
		return new Rect (input.x < 0 ? 0 : parent.x,
		                 input.y < 0 ? 0 : parent.y,
		                 (input.width + parent.x) < (parent.width + parent.x) ?
		                 	input.width : input.width - (parent.width - parent.x),
		                 (input.height + parent.y) < (parent.height + parent.y) ?
		                 	input.height : input.height - (parent.height - parent.y));
	}
	
	Rect relRec (Rect rel, Rect input, relRecMode mode = relRecMode.floating) {
		return relRec(rel, input.x, input.y, input.width, input.height, mode);
	}


	void OnGUI () {
		EditorGUI.LabelField (new Rect(0,0,position.width,20),"Dialogue Editor");
		dia = (DialogueTree)EditorGUI.ObjectField (new Rect(0,15,200,15),"", dia, typeof(DialogueTree), false);
		
		if (dia == null) return;
		
		if (GUI.Button(new Rect(200,15,200,20), "Add node.")) addItem(); 
		
		names = getNames();
		
		
		//new Rect(1,15,boxWidth-1, boxHeight-15)
		boxHeight = 15+(dia.nodes.Length/2)*15;
		
		DiaBox = new Rect (5, 35, position.width - 10, position.height - 40);

		GUI.Box (relRec(DiaBox, 0,0,DiaBox.width,DiaBox.height), "");
		
		BeginWindows();
		for (int i = 0; i < dia.nodes.Length; i++) {
			dia.nodes[i].nextNode = dia.nodes[i].nextNode % dia.nodes.Length;
			
			dia.nodes[i].nodePos = GUI.Window(i,dia.nodes[i].nodePos, nodeWindow,"");
			Drawing.curveFromTo(dia.nodes[i].nodePos, dia.nodes[dia.nodes[i].nextNode].nodePos,
			                    Color.green, new Color(0,0,0,0));
		}
		//(dia.nodes[i], DiaBox, 0, 0)
		EndWindows();
	}
	
	/// <summary>
	/// Draws a node inside of DiaBox at (x,y).
	/// </summary>
	/// <param name="node">The node to draw.</param>
	/// <param name="diaBox">The box in the editor.</param>
	/// <param name="x">The local X coordinate to place the LHS of the box at.</param>
	/// <param name="y">The local Y coordinate to place the top of the box at.</param>
	//void nodeWindow(DialogueNode node, Rect diaBox, float x, float y) {
	void nodeWindow(int i) {
		GUI.Label(new Rect(0,0,boxWidth,boxHeight), dia.nodes[i].name);
		//dia.nodes[i].nextNode = GUI.Toolbar(new Rect(5,10,boxWidth-10, 20), dia.nodes[i].nextNode, names);
		dia.nodes[i].nextNode = GUI.SelectionGrid(new Rect(1,15,boxWidth-1, boxHeight-15), dia.nodes[i].nextNode, names, 2);
		if (GUI.Button(new Rect(boxWidth-11,0,10,10), "x")) removeItem(i);
		GUI.DragWindow();
	}
	
	void addItem () {
		System.Array.Resize<DialogueNode>(ref dia.nodes, dia.nodes.Length+1);
		dia.nodes[dia.nodes.Length-1] = new DialogueNode();
		dia.nodes[dia.nodes.Length-1].nodePos = new Rect (100,100,boxWidth,boxHeight);
	}
	
	void removeItem (int j) {
		for (int i = j+1; i < dia.nodes.Length; i++) dia.nodes[i] = dia.nodes[i-1];
		// TODO: THIS WILL BREAK EVVVVVVERYTHING LATER!
		for (int i = 0; i < dia.nodes.Length; i++) {
			if (dia.nodes[i].nextNode == j) dia.nodes[i].nextNode = i;
			if (dia.nodes[i].nextNode > i) dia.nodes[i].nextNode --;
		}
		System.Array.Resize<DialogueNode>(ref dia.nodes, dia.nodes.Length-1);
	}
}

enum relRecMode {
	clamp,			/// Clamps the rectangle to within the parent.
	floating,		/// Allows the rectangle to leave the bounds of the parent.
	singleEdgeClamp	/// Clamps the right and botttom eedges to within the parent.
}


