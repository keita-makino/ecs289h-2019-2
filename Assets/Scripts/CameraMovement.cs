using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  private Transform center;

  void Start()
  {
    center = new GameObject().transform;
    this.transform.parent = center;
  }
  void Update()
  {
    if (Input.GetMouseButton(0))
    {
      center.Rotate(Input.GetAxis("Mouse Y") * 10, Input.GetAxis("Mouse X") * 10, 0);
    }
  }
}
