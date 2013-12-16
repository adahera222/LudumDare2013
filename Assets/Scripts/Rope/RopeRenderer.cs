using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeRenderer : MonoBehaviour {

	public float RopeWidth;

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

		gameObject.AddComponent<MeshRenderer>();
		gameObject.AddComponent<MeshFilter>();

		ropemesh = new Mesh();
		GetComponent<MeshFilter>().mesh = ropemesh;
	}

	void Update () {


		vert.Clear();
		tris.Clear();
		uvs.Clear();

		for (int i = 0; i < roperef.ropePieces.Length; i++) {
			GameObject obj = roperef.ropePieces[i]; //ISN'T C GREAT

			int trioffset = vert.Count;

			Vector3 startpoint = obj.transform.TransformPoint(0, 0, -(roperef.segmentLength));
			Vector3 endpoint = obj.transform.TransformPoint(0, 0, (roperef.segmentLength));

			vert.Add(startpoint + new Vector3(0f, RopeWidth / 2, 0f));
			vert.Add(startpoint + new Vector3(0f, -(RopeWidth / 2), 0f));
			vert.Add(endpoint + new Vector3(0f, RopeWidth / 2, 0f));
			vert.Add(endpoint + new Vector3(0f, -(RopeWidth / 2), 0f));

			//CONTINUE FROM LAST SEGMENT
			if(i != 0) {
				tris.Add(trioffset - 2);
				tris.Add(trioffset - 1);
				tris.Add(trioffset + 1);

				tris.Add(trioffset + 1);
				tris.Add(trioffset);
				tris.Add(trioffset - 2);
			}
			
			//THINGS IN THE MIDDLE
			tris.Add(trioffset);
			tris.Add(trioffset + 1);
			tris.Add(trioffset + 2);

			tris.Add(trioffset + 1);
			tris.Add(trioffset + 3);
			tris.Add(trioffset + 2);

			uvs.Add(new Vector2(1,1));
			uvs.Add(new Vector2(1,1));
			uvs.Add(new Vector2(1,1));
			uvs.Add(new Vector2(1,1));
		}

		ropemesh.triangles = tris.ToArray();
		ropemesh.vertices = vert.ToArray();
		ropemesh.uv = uvs.ToArray();

		//attach to filter

		//GetComponent<MeshRenderer>().Render(0);
	}
}
