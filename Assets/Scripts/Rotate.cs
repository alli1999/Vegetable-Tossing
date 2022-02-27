using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SystemInfo.supportsGyroscope)
        {
            transform.rotation = Quaternion.Euler(-90, 0, 0) * GyroRotate(Input.gyro.attitude);
        }
    }

    private Quaternion GyroRotate(Quaternion q)
    {
        return new Quaternion(-q.x, q.y, q.z, q.w);
    }
}
