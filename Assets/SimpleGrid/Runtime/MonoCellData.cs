using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JYSimpleGrid.Runtime
{
    public enum CellType
    {
        Path,
        Obstacle,
    }


    public class MonoCellData
    {
        public CellType cellType;
        public int weight;
    }
}
