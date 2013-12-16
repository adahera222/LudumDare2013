using UnityEngine;
using System.Collections;

public class GrabDisplay : MonoBehaviour {

	public Texture activemat;
	public Texture inactivemat;

	Texture mat;

	void OnGUI() {
		if(GetComponent<RopeGrabber>().isGrabbing) mat = activemat;
		else mat = inactivemat;

		GUI.DrawTexture(new Rect(Screen.width/2-16, Screen.height/2-16, 32, 32), mat);
	}
}
