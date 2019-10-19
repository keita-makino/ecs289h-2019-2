using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
  public bool[] isAvailabe = new bool[3] { true, true, true };
  private UnityEngine.UI.Text[] texts;

  public void Awake()
  {
  }

  void Start()
  {
    string[] strs = new string[3] { "Click-Drag to Rotate.", "Wheel to Zoom.", "Mouseover for Detail." };
    Vector3 position = new Vector3(-Screen.width / 2 + 60, -Screen.height / 2 + 24, 0);


    GameObject canvasObject = new GameObject();
    canvasObject.AddComponent<Canvas>();
    Canvas canvas = canvasObject.GetComponent<Canvas>();
    canvas.renderMode = RenderMode.ScreenSpaceOverlay;

    texts = new UnityEngine.UI.Text[3];
    for (int i = 0; i < strs.Length; i++)
    {
      GameObject textObject = new GameObject();
      textObject.transform.parent = canvasObject.transform;

      texts[i] = textObject.AddComponent<UnityEngine.UI.Text>();
      texts[i].font = Resources.GetBuiltinResource<Font>("Arial.ttf");
      texts[i].fontSize = 16;
      texts[i].color = Color.black;
      texts[i].horizontalOverflow = HorizontalWrapMode.Overflow;
      texts[i].text = strs[i];

      RectTransform transform = texts[i].GetComponent<RectTransform>();
      transform.localPosition = position - new Vector3(0, i * 24, 0);
    }
  }

  void Update()
  {
    for (int i = 0; i < texts.Length; i++)
    {
      texts[i].enabled = isAvailabe[i];
    }

    if (Input.GetMouseButtonDown(0))
      isAvailabe[0] = false;

    if (Input.GetAxis("Mouse ScrollWheel") != 0)
      isAvailabe[1] = false;

    if (Input.GetMouseButtonDown(0))
      isAvailabe[0] = false;
  }
}
