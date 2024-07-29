//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class WallController : MonoBehaviour
{
    [Header("���� ���� ����")]
    public bool gameStart;

    [Header("Z�� ���� ���� ���� Transform")]
    [SerializeField] private Transform eastWallTransform;

    [Header("Z�� ���� ���� ���� Transform")]
    [SerializeField] private Transform westWallTransform;

    [Header("���� ������ �پ��µ� �ɸ��� �ð�")]
    [SerializeField] private float maxTime = 600f; //10 minuites

    [Header("���� ��� �ð�")]
    [SerializeField] private float currentTime = 0f;

    [Header("���� ���� �پ��� �ð�")]
    [SerializeField] private float narrowTime = 60f;

    [Header("���� �پ��� ������")]
    [SerializeField] private int phase = 1;

    [Header("���� �پ��� �ӵ�")]
    [SerializeField] private float narrowSpeed;

    [Header("���� ���� �پ�� �� �ִ��� ����")]
    [SerializeField] private bool canNarrow = true;

    private void Awake()
    {
        //Initialize variables
        maxTime = 600f;
        currentTime = 0f;
        narrowTime = 60f;

        phase = 1;

        canNarrow = true;
    }
    private void Update()
    {
        if (gameStart)
        {
            currentTime+= Time.deltaTime;
            
            //Check Narrow Time
            if (currentTime >= narrowTime)
            {
                if (narrowTime != maxTime)
                    //Convert Narrow Sign Method
                    ConvertNarrowSign();
                else
                    //GameOver
                    Debug.Log("���� ����");
            }

            //Move Walls
            if (!canNarrow)
            {
                //Call Narrow Walls Method
                NarrowWalls();
            }
        }
    }
    //Convert Narrow Sign
    private void ConvertNarrowSign()
    {
        if (canNarrow)
        {
            //Avioid Duplication
            canNarrow = false;
        }
    }
    private void NarrowWalls()
    {
        //Move Walls
        eastWallTransform.Translate(Vector3.left * narrowSpeed * Time.deltaTime, Space.Self);
        westWallTransform.Translate(Vector3.right * narrowSpeed * Time.deltaTime, Space.Self);

        Vector3 eastWallDes = new Vector3(7 - (0.7f * phase), eastWallTransform.position.y, eastWallTransform.position.z);
        Vector3 westWallDes = new Vector3(((-1) * 7) - (0.7f * phase), westWallTransform.position.y, westWallTransform.position.z);

        if (Vector3.Distance(eastWallTransform.localPosition, eastWallDes) < 0.2f)
        {
            //Increase Phase
            phase++;

            //Update Narrow Time
            narrowTime = 60f * phase;

            //Update Sign
            canNarrow = true;
        }
    }
}
