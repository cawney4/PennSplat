﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrailRender : MonoBehaviour {

	public LineRenderer line;
	public SimpleController character;

	public List<Vector3> positions;

	// Use this for initialization
	void Start () {
		
		positions = new List<Vector3> ();
        character = null;
    }
	
	// Update is called once per frame
	void Update () {
        if(character == null)
        {
            character = (SimpleController)FindObjectOfType(typeof(SimpleController));
        }
        positions = character.positions;
		Vector3[] pos = new Vector3[positions.Count];
		for (int i = 0; i < positions.Count; i++) {
			pos [i] = new Vector3 (positions [i].x, 1, positions [i].z);
		}

		line.SetVertexCount (positions.Count);
		line.SetPositions (pos);
		line.SetColors (new Color (1, 0, 0), new Color (1, 0, 0));
		line.material.color = new Color (1, 0, 0);
		line.SetWidth (1, 1);

	}
}
