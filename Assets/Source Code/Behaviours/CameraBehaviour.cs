using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script will move camera along the z axis as the player moves.
/// Manually enter x and y positions, and rotation of the camera.
/// </summary>
public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTarget;
    private Transform cameraTransform;
    private Vector3 movementVector;
    [SerializeField]
    [Range(-30f, 30f)]
    private float zAxisOffset = 10f;

    private void Start()
    {
        cameraTransform = this.GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        //Adjust the new vector as the character moves forward.
        movementVector = new Vector3(cameraTransform.position.x, cameraTransform.position.y, (cameraTarget.position.z + zAxisOffset));
        cameraTransform.position = movementVector;
    }
}
