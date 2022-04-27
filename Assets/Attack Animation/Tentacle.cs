using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int Length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;
    public float smoothSpeed;
    public float trailSpeed;

    public Transform targetDir;
    public float targetDist;

    public float wiggleSpeed, wiggleMagnitude;
    public Transform wiggleDir;
    private void Start()
    {
        lineRend.positionCount = Length;
        segmentPoses = new Vector3[Length];
        segmentV = new Vector3[Length];
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed+i/trailSpeed);
        }
        lineRend.SetPositions(segmentPoses);
    }
}
