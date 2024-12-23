using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    enum CameraState {Following, Bonus}
    private CameraState cameraState;

    [Header(" Cameras ")]
    [SerializeField] private GameObject followCam;
    [SerializeField] private GameObject bonusCam;
    [SerializeField] private CinemachineTargetGroup targetGroup; 

    [Header(" Elements ")]
    [SerializeField] private Transform runnersParent;


    private void Awake()
    {
        SquadDetection.OnFinishLineCrossed += EnableBonusCam;    
    }

    private void OnDestroy()
    {
        SquadDetection.OnFinishLineCrossed -= EnableBonusCam;        
    }

    private void Start()
    {
        cameraState = CameraState.Following;
    }

    private void Update()
    {
        ManageCameraState();
    }

    private void ManageCameraState()
    {
        switch(cameraState)
        {
            case CameraState.Bonus:
                ManageBonusState();
            break;
        }
    }

    List<CinemachineTargetGroup.Target> bonusCamTargets = new();
    private void ManageBonusState()
    {
        bonusCamTargets.Clear();

        for (int i = 0; i < runnersParent.childCount; i++)
        {
            CinemachineTargetGroup.Target runnerTarget = new();
            runnerTarget.target = runnersParent.GetChild(i);
            runnerTarget.weight = 1;
            runnerTarget.radius = 1;

            bonusCamTargets.Add(runnerTarget);
        }

        targetGroup.m_Targets = bonusCamTargets.ToArray();
    }

    private void EnableBonusCam()
    {
        followCam.SetActive(false);
        bonusCam.SetActive(true);

        cameraState = CameraState.Bonus;
    }
}
