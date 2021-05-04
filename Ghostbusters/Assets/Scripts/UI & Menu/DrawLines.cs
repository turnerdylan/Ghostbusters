using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    private LineRenderer lr;
    int numPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        numPoints = DataSelectManager.Instance.furthestUnlockedLevel;

        lr.positionCount = numPoints;


        lr.startWidth = .1f;
        lr.endWidth = .1f;
        if(numPoints < 2) numPoints = 2;
        for (int i=0; i < numPoints; i++)
        {
            Draw(DataSelectManager.Instance.levelPins[i].transform.position, DataSelectManager.Instance.levelPins[i+1].transform.position, i);
        }
        
    }

    public void Draw(Vector3 pos1, Vector3 pos2, int index)
    {
        if (index + 1 > lr.positionCount - 1) return;

        GameObject myLine = new GameObject();
        myLine.transform.position = pos1;
        lr.SetPosition(index, pos1);
        lr.SetPosition(index + 1, pos2);
    }
}
