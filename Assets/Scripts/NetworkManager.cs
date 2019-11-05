using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

[System.Serializable]
public class Node
{
  public string id;
  public int group;
  public string color;
  public float x;
  public float y;
  public float z;
  public Vector3 position
  {
    get { return new Vector3(this.x, this.y, this.z) * 15f; }
  }
}

[System.Serializable]
public class Link
{
  public int source;
  public int target;
  public int value;
}

[System.Serializable]
public class Data
{
  public Node[] nodes;
  public Link[] links;
}

public class NetworkManager : MonoBehaviour
{
  private Data data;
  public GameObject parentObject;
  private GameObject[] sphereArray;
  private GameObject[] cylinderArray;
  public float scale { get; set; } = 1;
  public Quaternion rotation { get; set; } = Quaternion.Euler(0, 0, 0);
  public Vector3 position { get; set; } = Vector3.zero;
  public string path;
  public string str;
  private async Task<String> GetData()
  {
    if (path.Contains("://"))
    {
      Debug.Log(path);
      var request = UnityWebRequest.Get(this.path);
      await request.SendWebRequest();
      return request.downloadHandler.text;
    }
    else
    {
      return File.ReadAllText(path);
    }
  }

  async void Start()
  {
    path = Path.Combine(Application.streamingAssetsPath, "lesmis-3d.json");
    str = await GetData();
    Debug.Log(str);
    data = JsonUtility.FromJson<Data>(str);

    parentObject = new GameObject();
    parentObject.transform.localScale = new Vector3(scale, scale, scale);
    parentObject.transform.rotation = rotation;
    parentObject.transform.position = position;

    sphereArray = new GameObject[data.nodes.Length];
    for (int i = 0; i < data.nodes.Length; i++)
    {
      Node node = data.nodes[i];
      sphereArray[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      sphereArray[i].transform.parent = parentObject.transform;
      sphereArray[i].transform.position = node.position;
      sphereArray[i].transform.localScale = new Vector3(3, 3, 3);
      sphereArray[i].AddComponent<EventHandler>();
      sphereArray[i].GetComponent<EventHandler>().nodeId = node.id;
      sphereArray[i].GetComponent<EventHandler>().position = node.position;

      Color color;
      if (ColorUtility.TryParseHtmlString(node.color, out color))
      {
        sphereArray[i].GetComponent<Renderer>().material.color = color;
      }
    }

    cylinderArray = new GameObject[data.links.Length];
    for (int i = 0; i < data.links.Length; i++)
    {
      Link link = data.links[i];
      Node[] cNodes = new Node[2] { data.nodes[link.source], data.nodes[link.target] };
      Vector3 center = (cNodes[1].position + cNodes[0].position) / 2;
      float distance = Vector3.Distance(cNodes[0].position, cNodes[1].position);

      cylinderArray[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
      cylinderArray[i].transform.position = center;
      cylinderArray[i].transform.parent = parentObject.transform;
      cylinderArray[i].transform.rotation = Quaternion.FromToRotation(
        Vector3.up, cNodes[0].position - cNodes[1].position
      );
      cylinderArray[i].transform.localScale = new Vector3(
        distance / 20, distance / 2, distance / 20
      );

    }
  }
}

public class EventHandler : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
  public string nodeId { get; set; }
  public Vector3 position { get; set; }
  private TextManager textManager;
  private NetworkManager networkManager;

  public void Awake()
  {
    textManager = GameObject.Find("TextManager").GetComponent<TextManager>();
    networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    // TooltipManager = GameObject.Find("TooltipManager").GetComponent<TooltipManager>();
  }
  // public void OnMouseEnter()
  // {
  //   textManager.textValue = nodeId;
  //   if (!Input.GetMouseButton(0))
  //   {
  //     TooltipManager.isAvailabe[2] = false;
  //   }
  // }
  // public void OnMouseExit()
  // {
  //   textManager.textValue = "hogehoge";
  // }
  // public void OnMouseOver()
  // {
  //   Vector3 pos = Input.mousePosition;
  //   textManager.position = new Vector3(
  //       pos.x < (Screen.width / 2) ?
  //         Math.Max(pos.x - 50, (45 + nodeId.Length * 13) / 2) :
  //         Math.Min(pos.x + 50, Screen.width - (45 + nodeId.Length * 13) / 2),
  //       pos.y < (Screen.height / 2) ?
  //         Math.Max(pos.y - 50, 25) :
  //         Math.Min(pos.y + 50, Screen.height - 25),
  //       0
  //     ) - new Vector3(Screen.width / 2, Screen.height / 2, 0);
  // }
  public void OnPointerEnter(PointerEventData pointerEventData)
  {
    // Transform tr = networkManager.parentObject.transform;
    // textManager.position = tr.rotation * ((position * tr.localScale.x + tr.position));
    textManager.textValue = nodeId;
  }
  public void OnPointerExit(PointerEventData pointerEventData)
  {
    textManager.textValue = "hogehoge";
  }
}