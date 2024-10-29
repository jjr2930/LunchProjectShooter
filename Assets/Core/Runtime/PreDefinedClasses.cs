using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JYCore.Runtime
{
    [Serializable]
    public class Int2D : Serializable2DArray<int>
    {
        public Int2D(int x, int y) : base(x, y)
        {
        }
    }

    [Serializable]
    public class Float2D : Serializable2DArray<float>
    {
        public Float2D(int x, int y) : base(x, y)
        {
        }
    }

    [Serializable]
    public class String2D : Serializable2DArray<string>
    {
        public String2D(int x, int y) : base(x, y)
        {
        }
    }

    [Serializable]
    public class Bool2D : Serializable2DArray<bool>
    {
        public Bool2D(int x, int y) : base(x, y)
        {
        }
    }

    //long version
    [Serializable]
    public class Long2D : Serializable2DArray<long>
    {
        public Long2D(int x, int y) : base(x, y)
        {
        }
    }

    //double version
    [Serializable]
    public class Double2D : Serializable2DArray<double>
    {
        public Double2D(int x, int y) : base(x, y)
        {
        }
    }
}
