using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity;
    private CharacterController _characterController;
    private float upDown;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * _speed;
        float vertical = Input.GetAxis("Vertical") * _speed;
        if (_characterController.isGrounded)
        {
            upDown = -_gravity * Time.deltaTime;
        }
        else
        {
            upDown -= _gravity * Time.deltaTime;
        }
        Vector3 movement = new Vector3(horizontal, upDown, vertical);
        _characterController.Move(movement * Time.deltaTime);
    }
}
