using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static bool DrawVertices = false;

    void OnGUI()
    {
        DrawVertices = GUILayout.Toggle(DrawVertices, "Draw vertices");
    }
}
