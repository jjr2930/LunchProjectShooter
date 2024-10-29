using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JYCore.Runtime;
using System;

namespace JYSimpleGrid.Runtime
{
    
    public class MonoGrid : MonoBehaviour
    {
        [SerializeField] Vector2Int cellCount = new Vector2Int(128,128);
        [SerializeField] float cellSize = 1f;
        [SerializeField] Grid<MonoCellData> grid;
        [SerializeField] MonoNode nodePrefab;

        public void Awake()
        {
            if(null == grid)
            {
                grid = new Grid<MonoCellData>(cellCount.x, cellCount.y, cellSize, transform.position);
            }
        }
        public void RefreshGrid()
        {
            grid = new Grid<MonoCellData>(cellCount.x, cellCount.y, cellSize, transform.position);
        }
    }
}
