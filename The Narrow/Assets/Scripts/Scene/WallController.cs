//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[DisallowMultipleComponent]
public class WallController : MonoBehaviour
{
    [Header("게임 시작 여부")]
    public bool gameStart;

    [Header("Z축 기준 동쪽 벽의 Transform")]
    [SerializeField] private Transform eastWallTransform;

    [Header("Z축 기준 서쪽 벽의 Transform")]
    [SerializeField] private Transform westWallTransform;

    [Header("벽이 완전히 줄어드는데 걸리는 시간")]
    [SerializeField] private float maxTime = 600f; //10 minuites

    [Header("현재 경과 시간")]
    [SerializeField] private float currentTime = 0f;

    [Header("다음 벽이 줄어드는 시간")]
    [SerializeField] private float narrowTime = 60f;

    [Header("벽이 줄어드는 페이즈")]
    [SerializeField] private int phase = 1;

    [Header("벽이 줄어드는 속도")]
    [SerializeField] private float narrowSpeed;

    [Header("현재 벽이 줄어들 수 있는지 여부")]
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
                    Debug.Log("게임 오버");
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
