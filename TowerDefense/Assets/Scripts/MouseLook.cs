using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public float distanceFromTarget = 1000;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .15f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;



    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

		if (target != null) {
			transform.position = target.position - transform.forward * distanceFromTarget;
		}
    }
}

//public class MouseLook : MonoBehaviour
//{

//    [AddComponentMenu("Camera-Control/Mouse Look")]

//    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
//    public RotationAxes axes = RotationAxes.MouseXAndY;
//    public float sensitivityX = 15F;
//    public float sensitivityY = 15F;
//    public float minimumX = -360F;
//    public float maximumX = 360F;
//    public float minimumY = -60F;
//    public float maximumY = 60F;
//    float rotationY = 0F;
//    Rigidbody rb;

//    void Update()
//    {
//        if (axes == RotationAxes.MouseXAndY)
//        {
//            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

//            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
//        }
//        else if (axes == RotationAxes.MouseX)
//        {
//            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
//        }
//        else
//        {
//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

//            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
//        }
//    }

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();
//        // Make the rigid body not change rotation
//        if (rb)
//            rb.freezeRotation = true;
//    }
//}

