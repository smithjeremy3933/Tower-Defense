﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject tile;
    [SerializeField] GameObject TowerViewPrefab;
    Node m_node;


    [Range(0, 0.5f)]
    public float borderSize = 0.15f;

    public void Init(Node node)
    {
        if (tile != null)
        {
            gameObject.name = "Node (" + node.xIndex + "," + node.yIndex + ")";
            gameObject.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f - borderSize, 1f, 1f - borderSize);
            m_node = node;
        }
    }

    void ColorNode(Color color, GameObject go)
    {
        if (go != null)
        {
            Renderer goRenderer = go.GetComponent<Renderer>();

            if (goRenderer != null)
            {
                goRenderer.material.color = color;
            }
        }
    }

    public void ColorNode(Color color)
    {
        ColorNode(color, tile);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && m_node.nodeType != NodeType.Blocked)
        {
            Instantiate(TowerViewPrefab, m_node.position, Quaternion.identity);
            m_node.nodeType = NodeType.Blocked;
        }
      
    }
}
