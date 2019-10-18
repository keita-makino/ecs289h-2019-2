using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  private bool isClicked = false;
  private Vector3 sourcePosition;

  void Update()
  {
    Ray ray = Camera.main.ScreenPointToRay(new Vector3(200, 200, 0));
    Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
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
