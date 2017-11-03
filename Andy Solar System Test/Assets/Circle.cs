// https://forum.unity3d.com/threads/linerenderer-to-create-an-ellipse.74028/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// To draw an orbit circle using lineRenderer
/// </summary>
public class Circle : MonoBehaviour
{
	public int segments = 48;
	public float xradius;
	public float yradius;
	LineRenderer line;

	void Start ()
	{
        line = gameObject.GetComponent<LineRenderer>();//TODO:

        line.positionCount = (segments + 1);
        line.useWorldSpace = false;
        line.startWidth = (1);
        line.endWidth = (1);
        CreatePoints();
    }


	public void CreatePoints ()
	{
        
        float x;
		float z;
		float y = 0f;

		float angle = 20f;

		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
			z = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius;

			line.SetPosition (i,new Vector3(x,y,z) );

			angle += (360f / segments);
		}
	}
}