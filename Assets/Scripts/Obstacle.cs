using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public Transform wallLeft;
    public Transform wallRight;

    public float holeSize = 2f;
    public float holeCenter = 0f;


    public float screenWidth = 20;

    public float DespawnY = -6.0f;

    void Awake()
    {
        ActorManager.Add(gameObject);
        SetupObstacle(holeSize, holeCenter);
    }

    private void OnDestroy()
    {
        ActorManager.Remove(gameObject);
    }

    void Update()
    {
    }

    public void SetupObstacle(float size, float location)
    {
        float holeSize = size;
        float holeCenter = location;


        // Calculate wall widths
        float leftWidth = (screenWidth / 2f) + holeCenter - (holeSize / 2f);
        float rightWidth = (screenWidth / 2f) - holeCenter - (holeSize / 2f);

        // Prevent negative values
        leftWidth = Mathf.Max(0.1f, leftWidth);
        rightWidth = Mathf.Max(0.1f, rightWidth);

        //  Debug.Log("RP:  " +( screenWidth / 2f - rightWidth / 2f));
        //Debug.Log("SW:" + screenWidth + "LW:" + leftWidth + "LP:" + (-screenWidth / 2f + leftWidth / 2f));
        // Position left wall
        wallLeft.localScale = new Vector3(leftWidth, wallLeft.localScale.y, 1);
        wallLeft.localPosition = new Vector3(
            -screenWidth / 2f + leftWidth / 2f,
            0,
            0
        );

        //  wallLeft.localPosition = new Vector3(
        //    0,
        //    0,
        //    0
        //);

        // Position right wall
        wallRight.localScale = new Vector3(rightWidth, wallRight.localScale.y, 1);
        wallRight.localPosition = new Vector3(
            screenWidth / 2f - rightWidth / 2f,
            0,
            0
        );
    }


    // Call this to randomize hole
    public void RandomizeHole(float minSize, float maxSize)
    {
        holeSize = Random.Range(minSize, maxSize);

        float maxOffset = (screenWidth / 2f) - (holeSize / 2f);

        holeCenter = Random.Range(-maxOffset, maxOffset);

        SetupObstacle(holeSize, holeCenter);
    }
}