using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePoint : MonoBehaviour
{
    public Vector3 Position; 
    public Quaternion Rotation; 

    public TimePoint(Vector3 Position, Quaternion Rotation)
    {
        this.Position = Position;
        this.Rotation = Rotation;
    }
}
