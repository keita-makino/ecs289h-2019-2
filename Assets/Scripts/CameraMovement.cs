using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

  [SerializeField]
  private Transform center;
  private float _moveSpeed = 3;
  public OVRInput.Controller controllerPrimary;
  public OVRInput.Controller controllerSecondary;
  private bool isPrimaryIndexTriggered = false;
  private bool isSecondaryIndexTriggered = false;
  private bool isPrimaryInitialized = false;
  private bool isSecondaryInitialized = false;
  private Vector3 primaryOrigin;
  private Vector3 secondaryOrigin;
  private float initialDistance;
  private Quaternion initialRotation;
  private Vector3 initialPosition;
  public Transform trackingSpace;

  [SerializeField]
  private NetworkManager networkManager;
  private float initialTargetScale;
  private Quaternion initialTargetRotation;
  private Vector3 initialtargetPosition;

  private void Start()
  {

  }
  private void Reset()
  {
    center = transform.Find("TrackingSpace/CenterEyeAnchor");

  }

  private void Update()
  {
    isPrimaryIndexTriggered = OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
    isSecondaryIndexTriggered = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
    if (isPrimaryIndexTriggered && isSecondaryIndexTriggered)
    {
      if (!isSecondaryInitialized)
      {
        primaryOrigin = trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(controllerPrimary));
        secondaryOrigin = trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(controllerSecondary));
        initialDistance = Vector3.Distance(primaryOrigin, secondaryOrigin);
        initialRotation = Quaternion.LookRotation(primaryOrigin - secondaryOrigin);
        initialPosition = (primaryOrigin + secondaryOrigin) / 2;

        initialTargetScale = networkManager.parentObject.transform.localScale.x;
        initialTargetRotation = networkManager.parentObject.transform.rotation;
        initialtargetPosition = networkManager.parentObject.transform.position;
        isPrimaryInitialized = true;
        isSecondaryInitialized = true;
      }
      else
      {
        Vector3 primaryPosition = trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(controllerPrimary));
        Vector3 secondaryPosition = trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(controllerSecondary));

        float currentDistance = Vector3.Distance(primaryPosition, secondaryPosition);
        Quaternion currentRotation = Quaternion.LookRotation(primaryPosition - secondaryPosition);
        Vector3 currentPosition = (primaryPosition + secondaryPosition) / 2;

        networkManager.parentObject.transform.localScale = new Vector3(1, 1, 1) * initialTargetScale * currentDistance / initialDistance;
        networkManager.parentObject.transform.rotation = initialTargetRotation * initialRotation * Quaternion.Inverse(currentRotation);
        networkManager.parentObject.transform.position = initialtargetPosition + (currentPosition - initialPosition) * 100 * networkManager.parentObject.transform.localScale.x;
      }
    }
    else
    {
      isPrimaryInitialized = false;
      isSecondaryInitialized = false;
    }

    Vector2 input = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
    transform.position += center.rotation * new Vector3(input.x, 0, input.y);
  }
}
