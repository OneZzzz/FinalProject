using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathpoint : MonoBehaviour
{
    public List<Transform> pathpointRaw = new List<Transform>();
    public List<Vector3> pathpoints = new List<Vector3>();
    private bool cycle = false;

    int currentIndex=0;
    int nextIndex=1;

    [SerializeField] float delayCheck = 0.5f;
    float delayCount = 0;
    public bool playerMoving = false;

    void OnEnable()
    {
        foreach (Transform t in pathpointRaw) pathpoints.Add(t.position);
    }

    public void setCycle(bool setTo)
    {
        cycle = setTo;
    }

    private void Update()
    {
        delayCount += Time.deltaTime;
        if ((playerMoving || (delayCount != 0 && delayCount > delayCheck)) && (transform.position - NextPoint()).magnitude < 0.1)
        {
            AdvancePoint();
        }
    }

    void AdvancePoint()
    {
        delayCount = 0;
        if (!cycle)
        {
            if (currentIndex < nextIndex) //going forward
            {
                currentIndex++;
                if (currentIndex == pathpoints.Count - 1) nextIndex--;
                else nextIndex++;
            }
            else //going backward
            {
                currentIndex--;
                if (currentIndex == 0) nextIndex++;
                else nextIndex--;
            }
        }
        else if (cycle)
        {
            currentIndex++;
            if(nextIndex == 0)
            {
                currentIndex = 0;
                nextIndex++;
            }
            else if (currentIndex == pathpoints.Count - 1) nextIndex = 0;
            else nextIndex++;
        }
    }

    public void SetPoint(int cur, int nex)
    {
        currentIndex = cur;
        nextIndex = nex;
    }

    public int CurrentIndex() { return currentIndex; }
    public int NextIndex() { return nextIndex; }
    public Vector3 CurrentPoint() { return pathpoints[currentIndex]; }
    public Vector3 NextPoint() { return pathpoints[nextIndex]; }

    //
    //����ϵ���һֱ��delta����next-currnet���������ƶ������within margin ofһ��point���Ǿ���transition mode����ʱ�ж���һ��point�����ߵ�point˭��delta�Ƕȸ�С�����ߵ�˭����ȥ��

}
