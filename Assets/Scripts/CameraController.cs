using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform SnakeHead;
    [SerializeField] private float TargetCamSpeed;
    [SerializeField] private float MaxCamSpeed;
    [SerializeField] private float AccelerationCamSpeed;
    [SerializeField] private float CamDistanceFollow;
    private Vector3 CamOffset = new Vector3(-5,13,1);
    private float CamSpeed;
    void Start()
    {
        CamSpeed = TargetCamSpeed;
        transform.position = SnakeHead.position + CamOffset;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(new Vector3(transform.position.x - CamOffset.x, 0, transform.position.z - CamOffset.z), Vector3.up*10);
    }
    // Update is called once per frame
    void Update()
    {

        // –ассчитываем ускорение камеры от рассто€ни€ между камерой и головой змейки в плоскости xz
        float magnitude = Vector3.Magnitude
            (
              new Vector3(transform.position.x - CamOffset.x, 0, transform.position.z - CamOffset.z)
            - new Vector3(SnakeHead.position.x, 0, SnakeHead.position.z)
            );
        if (magnitude > CamDistanceFollow)
            CamSpeed += Time.deltaTime * AccelerationCamSpeed;
        else
            CamSpeed -= Time.deltaTime * AccelerationCamSpeed;
        CamSpeed = Mathf.Clamp(CamSpeed, TargetCamSpeed, MaxCamSpeed);
       
        Vector3 NewPosition = SnakeHead.position + CamOffset;
        transform.position = Vector3.Lerp(transform.position, NewPosition , Time.deltaTime * CamSpeed);

    }
}
