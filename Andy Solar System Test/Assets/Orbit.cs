using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public GameObject orbit;
    new public string name;
    public float radius;
    public Color color;
    public float width;
    LineRenderer line;
    

    public Orbit(string orbitName, float orbitRadius, Color orbitColor, float orbitWidth)
    {
        name = orbitName;
        radius = orbitRadius/3;
        color = orbitColor;
        width = orbitWidth;
        orbit = new GameObject();
        orbit.name = name;
        
        line = orbit.AddComponent<LineRenderer>();
        orbit.AddComponent<Circle>();
        
        orbit.GetComponent<Circle>().xradius = radius;
        orbit.GetComponent<Circle>().yradius = radius;

        line.startWidth = width*0.002f;
        line.endWidth = width*0.002f;
        line.loop = true;
        line.useWorldSpace = false;
        line.material.color = color;
    }
    public void updateRadius(float radiusMulti)
    {
        radius *= radiusMulti;
        orbit.GetComponent<Circle>().xradius = radius;
        orbit.GetComponent<Circle>().yradius = radius;
        orbit.GetComponent<Circle>().CreatePoints();
    }
    public void DestroyOrbit()
    {
        Destroy(orbit);
    }
}
