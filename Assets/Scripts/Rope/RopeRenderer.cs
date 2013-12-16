using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeRenderer : MonoBehaviour {

	public float RopeWidth;
	public Material mat;

	List<Vector3> vert;
	List<int> tris;
	List<Vector2> uvs;

	CreateRope roperef;
	Mesh ropemesh;

	void Start () {
		roperef = GetComponent<CreateRope>();
		vert = new List<Vector3>();
		tris = new List<int>();
		uvs = new List<Vector2>();

		if(!GetComponent<MeshRenderer>()) gameObject.AddComponent<MeshRenderer>();
		gameObject.AddComponent<MeshFilter>();

		ropemesh = new Mesh();
		GetComponent<MeshFilter>().mesh = ropemesh;
		Material[] NSFC = GetComponent<MeshRenderer>().materials;
		NSFC[0] = mat;
		GetComponent<MeshRenderer>().materials = NSFC;
	}

	void LateUpdate () {


		vert.Clear();
		tris.Clear();
		uvs.Clear();

		for (int i = 0; i < roperef.ropePieces.Length; i++) {
			GameObject obj = roperef.ropePieces[i]; //ISN'T C GREAT

			int trioffset = vert.Count;

			Vector3 startpoint = obj.transform.TransformPoint(0, 0, -(roperef.segmentLength / 2));
			Vector3 endpoint = obj.transform.TransformPoint(0, 0, (roperef.segmentLength / 2));

			Vector3 offset = new Vector3(0f, RopeWidth / 2, 0f);

			vert.Add(startpoint + offset);
			vert.Add(startpoint - offset);
			vert.Add(endpoint + offset);
			vert.Add(endpoint - offset);

			//CONTINUE FROM LAST SEGMENT
			if(i != 0) {
				//FORWARD
				tris.Add(trioffset - 2);
				tris.Add(trioffset - 1);
				tris.Add(trioffset + 1);

				tris.Add(trioffset + 1);
				tris.Add(trioffset);
				tris.Add(trioffset - 2);

				//BACKWARDS
				tris.Add(trioffset + 1);
				tris.Add(trioffset - 1);
				tris.Add(trioffset - 2);
				
				tris.Add(trioffset - 2);
				tris.Add(trioffset);
				tris.Add(trioffset + 1);
			}
			
			//THINGS IN THE MIDDLE
			//FORWARD
			tris.Add(trioffset);
			tris.Add(trioffset + 1);
			tris.Add(trioffset + 2);

			tris.Add(trioffset + 1);
			tris.Add(trioffset + 3);
			tris.Add(trioffset + 2);

			//BACKWARDS
			tris.Add(trioffset + 2);
			tris.Add(trioffset + 1);
			tris.Add(trioffset);
			
			tris.Add(trioffset + 2);
			tris.Add(trioffset + 3);
			tris.Add(trioffset + 1);

			uvs.Add(new Vector2(1,0));
			uvs.Add(new Vector2(0,0));
			uvs.Add(new Vector2(1,1));
			uvs.Add(new Vector2(0,1));
		}

		ropemesh.triangles = tris.ToArray();
		ropemesh.vertices = vert.ToArray();
		ropemesh.uv = uvs.ToArray();

		ropemesh.RecalculateBounds();
		ropemesh.RecalculateNormals();

		//attach to filter

		GetComponent<MeshRenderer>().Render(0);
	}
}
