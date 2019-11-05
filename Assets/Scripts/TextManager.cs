using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
  public TooltipManager TooltipManager;
  public LaserPointer laserPointer;
  private bool isAvailabe = true;
  private UnityEngine.UI.Text text;
  private UnityEngine.UI.Image image;
  public string textValue { get; set; } = "hgoehoge";
  public RectTransform rectTransform { get; set; }
  public Vector3 position { get; set; } = new Vector3(0, 0, 0);
  public Camera worldCamera;
  private GameObject canvasObject;
  void Start()
  {
    canvasObject = new GameObject();
    canvasObject.AddComponent<Canvas>();
    Canvas canvas = canvasObject.GetComponent<Canvas>();
    canvas.renderMode = RenderMode.WorldSpace;
    canvas.worldCamera = worldCamera;

    GameObject imageObject = new GameObject();
    imageObject.transform.parent = canvasObject.transform;
    image = imageObject.AddComponent<UnityEngine.UI.Image>();
    imageObject.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);

    GameObject textObject = new GameObject();
    textObject.transform.parent = imageObject.transform;
    textObject.transform.localScale = new Vector3(1, 1, 1);

    text = textObject.AddComponent<UnityEngine.UI.Text>();
    text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
    text.fontSize = 120;
    text.fontStyle = FontStyle.Bold;
    text.color = Color.black;
    text.alignment = TextAnchor.MiddleCenter;
    text.horizontalOverflow = HorizontalWrapMode.Overflow;
    text.verticalOverflow = VerticalWrapMode.Overflow;

    rectTransform = image.GetComponent<RectTransform>();
  }

  void Update()
  {
    isAvailabe = true;
    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
      isAvailabe = false;
    if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick) != Vector2.zero)
      isAvailabe = false;
    if (TooltipManager.isAvailabe)
      isAvailabe = false;
    image.enabled = (textValue != "hogehoge" && isAvailabe);
    text.enabled = image.enabled;

    Vector3 start = laserPointer._startPoint;
    Vector3 end = laserPointer._endPoint;

    position = start + Quaternion.Euler(12.5f, 0, 0) * ((end - start) / 2);
    text.text = textValue;
    rectTransform.sizeDelta = new Vector2(225 + textValue.Length * 65, 200);
    canvasObject.transform.position = position;
    canvasObject.transform.rotation = Quaternion.LookRotation(canvasObject.transform.position - worldCamera.transform.position);

  }
}
