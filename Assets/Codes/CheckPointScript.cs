using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem.Controls;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    public static Vector3 vectorPoint;

    [SerializeField] float deadPoint;

    private CharacterController controller;

    [SerializeField] PlayerKnockback knockBackPlayer;

    [SerializeField] CinemachineCamera vcam;    


    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        vectorPoint = player.transform.position;
    }

    void Update()
    {
        if (player.transform.position.y < -deadPoint)
        {
            knockBackPlayer.ResetKnockback();

            Vector3 oldpos = player.transform.position;

            controller.enabled = false;
            player.transform.position = vectorPoint + Vector3.up * 0.2f;
            controller.enabled = true;
            
            Vector3 delta = player.transform.position - oldpos;
            vcam.OnTargetObjectWarped(player.transform, delta);
            vcam.PreviousStateIsValid = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            vectorPoint = player.transform.position;
        }
    }
}
