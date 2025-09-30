using UnityEngine;

public class EllenPlayerController : PlayerController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetState(Constants.EPlayerState.Idle);
    }
}
