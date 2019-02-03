using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{

	// Update is called once per frame
	void FixedUpdate()
	{
		Screen.SetResolution(675, 900, false);
	}
}
