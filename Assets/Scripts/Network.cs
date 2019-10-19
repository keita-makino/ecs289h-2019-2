using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

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

public class Network : MonoBehaviour
{
  private Data data;
  private GameObject[] sphereArray;
  private GameObject[] cylinderArray;
  void Start()
  {
    string json = File.ReadAllText("Assets/Data/lesmis-3d.json");
    data = JsonUtility.FromJson<Data>(json);

    sphereArray = new GameObject[data.nodes.Length];
    for (int i = 0; i < data.nodes.Length; i++)
    {
      Node node = data.nodes[i];
      sphereArray[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      sphereArray[i].transform.position = node.position;
      sphereArray[i].transform.localScale = new Vector3(3, 3, 3);

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
      cylinderArray[i].transform.rotation = Quaternion.FromToRotation(
        Vector3.up, cNodes[0].position - cNodes[1].position
        );
      cylinderArray[i].transform.localScale = new Vector3(
        distance / 20, distance / 2, distance / 20
        );

    }
  }

  void Update() { }
}
// Update is called once per frame