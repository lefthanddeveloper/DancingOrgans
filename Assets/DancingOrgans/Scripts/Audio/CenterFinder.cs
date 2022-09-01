using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CenterFinder : MonoBehaviour
{
   
   public void FindCenterPosition()
	{
		Vector3 center = GetComponent<Renderer>().bounds.center;
		Debug.Log(center);
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(CenterFinder))]
public class CenterFinderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		var script = (CenterFinder)target;

		if(GUILayout.Button("Find Center"))
		{
			script.FindCenterPosition();
		}
	}
}
#endif
