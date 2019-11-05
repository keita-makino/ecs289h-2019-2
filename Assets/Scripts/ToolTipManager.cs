using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
  public bool isAvailabe = true;
  private UnityEngine.UI.Text text;
  private UnityEngine.UI.Image image;
  public Camera worldCamera;

  void Start()
  {
    string str = "Right-Joystick = WASD \n Two-Index-Triggers = Manipulate";
    GameObject canvasObject = new GameObject();
    canvasObject.AddComponent<Canvas>();
    Canvas canvas = canvasObject.GetComponent<Canvas>();
    canvas.renderMode = RenderMode.WorldSpace;
    canvas.worldCamera = worldCamera;

    GameObject imageObject = new GameObject();
    imageObject.transform.parent = canvasObject.transform;
    image = imageObject.AddComponent<UnityEngine.UI.Image>();
    imageObject.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

    GameObject textObject = new GameObject();
    textObject.transform.parent = imageObject.transform;
    textObject.transform.localScale = new Vector3(1, 1, 1);

    text = textObject.AddComponent<UnityEngine.UI.Text>();
    text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
    text.fontSize = 40;
    text.fontStyle = FontStyle.Bold;
    text.color = Color.black;
    text.alignment = TextAnchor.MiddleCenter;
    text.horizontalOverflow = HorizontalWrapMode.Overflow;
    text.verticalOverflow = VerticalWrapMode.Overflow;
    text.text = str;

    RectTransform rectTransform = image.GetComponent<RectTransform>();
    rectTransform.sizeDelta = new Vector2(225 + str.Length * 65, 200);
    rectTransform.localPosition = new Vector3(0, 0, -30);
  }

  void Update()
  {
    image.enabled = isAvailabe;
    text.enabled = isAvailabe;
    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
      isAvailabe = false;

    if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick) != Vector2.zero)
      isAvailabe = false;
  }
}
