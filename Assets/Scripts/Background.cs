using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float yPos = 0.0f;

    private void Update()
    {
        yPos += Constants.BackgroundYMvmtVel * Time.deltaTime;
        yPos %= 7.4f;
        Vector3 offset = new(0.0f, yPos, 0.0f);
        transform.position = offset;
    }
}