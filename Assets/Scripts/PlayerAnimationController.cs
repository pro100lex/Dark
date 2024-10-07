using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Source _source;

    private void Start()
    {
        _source = GameObject.Find("Source").GetComponent<Source>();
    }

    private void Update()
    {
        SetPlayerAnimationByType();
    }

    private void SetPlayerAnimationByType()
    {
        switch (_source.PlayerCurrentMovementName)
        {
            case "AirStaying":
                break;
            case "AirWalkingForward":
                break;
            case "AirWalkingBackward":
                break;
            case "AirWalkingSideways":
                break;
            case "AirRunningForward":
                break;
            case "AirRunningBackward":
                break;
            case "AirRunningSideways":
                break;

            case "SquatStaying":
                break;
            case "SquatWalkingForward":
                break;
            case "SquatWalkingBackward":
                break;
            case "SquatWalkingSideways":
                break;
            case "SquatRunningForward":
                break;
            case "SquatRunningBackward":
                break;
            case "SquatRunningSideways":
                break;

            case "FullGrowthStaying":
                _source.PlayerAnimator.SetBool("IsWalking", false);
                break;
            case "FullGrowthWalkingForward":
                _source.PlayerAnimator.SetBool("IsWalking", true);
                break;
            case "FullGrowthWalkingBackward":
                break;
            case "FullGrowthWalkingSidaways":
                break;
            case "FullGrowthRunningForward":
                break;
            case "FullGrowthRunningBackward":
                break;
            case "FullGrowthRunningSideways":
                break;

            default:
                break;
        }
    }
}
