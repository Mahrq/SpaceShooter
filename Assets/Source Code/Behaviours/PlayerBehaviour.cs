using UnityEngine;
using System.Collections;

public class PlayerBehaviour : Actor
{
    private Vector3 movementVector;
    private float hInput, vInput;


    private void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        movementVector = new Vector3(hInput, 0, vInput) * movementSpeed;
        actorTransform.Translate(movementVector);
        
    }


}
