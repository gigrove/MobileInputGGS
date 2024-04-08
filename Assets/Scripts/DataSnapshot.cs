using UnityEngine;

public class DataSnapshot 
{
    public float time;
    public float x;
    public float y;
    public float z;

    public DataSnapshot()
    {
        time = Time.timeSinceLevelLoad;
        x = Input.acceleration.x;
        y = Input.acceleration.y;
        z = Input.acceleration.z;
    }
}

