using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
  private UnityEngine.UI.Text text;
  private UnityEngine.UI.Image image;
  public string textValue { get; set; } = "hogehoge";
  public RectTransform rectTransform { get; set; }
  public Vector2 size { get; set; } = new Vector2(250, 60);
  public Vector3 position { get; set; } = new Vector3(0, 0, 0);
  void Start()
  {
    GameObject canvasObject = new GameObject();
    canvasObject.AddComponent<Canvas>();
    Canvas canvas = canvasObject.GetComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;

    GameObject imageObject = new GameObject();
    imageObject.transform.parent = canvasObject.transform;
    image = imageObject.AddComponent<UnityEngine.UI.Image>();

    GameObject textObject = new GameObject();
    textObject.transform.parent = imageObject.transform;

    text = textObject.AddComponent<UnityEngine.UI.Text>();
    text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
    text.fontSize = 24;
    text.fontStyle = FontStyle.Bold;
    text.color = Color.black;
    text.alignment = TextAnchor.MiddleCenter;
    text.horizontalOverflow = HorizontalWrapMode.Overflow;

    rectTransform = image.GetComponent<RectTransform>();
  }

  void Update()
  {
    if (Input.GetMouseButtonUp(0))
    {
      textValue = "hogehoge";
    }
    image.enabled = (textValue != "hogehoge") && !Input.GetMouseButton(0);
    text.enabled = image.enabled;
    text.text = textValue;
    rectTransform.sizeDelta = new Vector2(45 + textValue.Length * 13, 48);
    rectTransform.localPosition = position;
  }
}
