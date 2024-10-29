using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JYCore.Runtime;
using System;

namespace JYSimpleGrid.Runtime
{
    [Serializable]
    public class Node2D<T> : Serializable2DArray<Node<T>>
    {
        public Vector2Int index;
        T data;

        public Node2D(int width, int height) : base(width, height)
        {
        }
    }

    public class Grid<T>
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 originPosition;
        [SerializeField] Node2D<T> gridArray;

        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new Node2D<T>(width, height);

            for (int x = 0; x < gridArray.ToArray().Length; x++)
            {
                for (int y = 0; y < gridArray.ToArray().Length; y++)
                {
                    gridArray[x, y] = new Node<T>();
                }
            }
        }
        public Vector3 GetCenterPoint(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPosition;
        }

        public Vector3 GetIndex(Vector3 worldPosition)
        {
            return new Vector3(
                Mathf.FloorToInt((worldPosition - originPosition).x / cellSize),
                Mathf.FloorToInt((worldPosition - originPosition).y / cellSize)
            );
        }
    }
}
