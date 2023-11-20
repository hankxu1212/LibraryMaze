using UnityEngine;
using UnityEngine.Serialization;

public partial class Player
{
    [Header("Player Controller")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float turnSpeed = 30f;

    private CharacterController controller;
    private Vector3 moveDir;
    
    // move towards `moveDir` with speed
    public void Move(float speed)
    {
        Vector3 velocity = speed * Time.deltaTime * moveDir;
        velocity.y = 0;
        controller.Move(velocity);
    }

    // called when player is either moving or idle
    private void HandleMovement()
    {
        Vector3 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        if (inputVector == Vector3.zero)
            return;

        moveDir.x = inputVector.x;
        moveDir.z = inputVector.z;
        moveDir = virtualCamera.transform.forward * moveDir.z + virtualCamera.transform.right * moveDir.x; 
        // move
        Move(walkSpeed);
    }
}