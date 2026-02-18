using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Obstacle : MonoBehaviour
{
    public Transform wallLeft;
    public Transform wallRight;

    public float holeSize = 2f;          
    public float holeCenter = 0f;

    public float horizontalSize = 5f;

    public float screenWidth = 20;

     public float DespawnY = -6.0f;

    void Awake()
    {
        SetupObstacle(holeSize, holeCenter);
    }

    void Update()
    {
    }

    public void SetupHorizontal(float size)
    {
        float holeSize = -5;
        float holeCenter = 0;
        

        // Calculate wall widths
        float leftWidth = (screenWidth / 2f) + holeCenter - (holeSize / 2f);
        float rightWidth = (screenWidth / 2f) - holeCenter - (holeSize / 2f);

        // Prevent negative values
        leftWidth = Mathf.Max(0.1f, leftWidth);
        rightWidth = Mathf.Max(0.1f, rightWidth);

        bool leftHigher = Random.value < 0.5f;

        float leftY;
        float rightY;


        if (leftHigher)
        {
            leftY = size/2;
            rightY = -size / 2;
        }
        else { 
            leftY = - size / 2;
            rightY = size / 2;
        }


        wallLeft.localScale = new Vector3(leftWidth, wallLeft.localScale.y, 1);
        wallLeft.localPosition = new Vector3(
            -screenWidth / 2f + leftWidth / 2f,
            leftY,
            0
        );

        wallRight.localScale = new Vector3(rightWidth, wallRight.localScale.y, 1);
        wallRight.localPosition = new Vector3(
            screenWidth / 2f - rightWidth / 2f,
            rightY,
            0
        );

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
