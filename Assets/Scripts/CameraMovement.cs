using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  private bool isClicked = false;
  private Vector3 sourcePosition;

  void Update()
  {
    if (Input.GetMouseButton(0))
    {
      if (isClicked)
      {

      }
      else
      {
        isClicked = true;

      }
    }
  }
}
