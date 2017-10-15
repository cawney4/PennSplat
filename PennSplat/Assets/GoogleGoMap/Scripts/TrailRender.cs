using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrailRender : MonoBehaviour {

	public LineRenderer line;
	public GameManager game;

	public List<Vector3> positions;

	// Use this for initialization
	void Start () {
		positions = new List<Vector3> ();
	}
	
    /*
	// Update is called once per frame
	void Update () {
		positions = game.playerPositions;
		Vector3[] pos = new Vector3[positions.Count];
		for (int i = 0; i < positions.Count; i++) {
			pos [i] = new Vector3 (positions [i].x, 1, positions [i].z);
		}

		line.SetVertexCount (positions.Count);
		line.SetPositions (pos);
		line.SetColors (new Color (1, 0, 0), new Color (1, 0, 0));
		line.SetWidth (1, 1);

	}
    */
}
