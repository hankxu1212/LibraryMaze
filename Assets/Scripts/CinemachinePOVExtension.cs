using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    private Vector3 startingRotation;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage,
        ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 deltaInput = GameInput.Instance.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * deltaTime;
                startingRotation.y += Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}