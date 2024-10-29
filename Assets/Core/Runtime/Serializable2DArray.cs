using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JYCore.Runtime
{
    [Serializable]
    public class Serializable2DArray<T>
    {
        [SerializeField] protected T[] array;

        public Serializable2DArray(int column, int row)
        {
            array = new T[column*row];
        }

        //복사 생성자
        public Serializable2DArray(Serializable2DArray<T> other)
        {
            array = new T[other.array.Length];
            Array.Copy(other.array, array, other.array.Length);
        }

        public T this[int column, int row]
        {
            get => array[row * column + column];
            set => array[row * column + column] = value;
        }

        public T[] ToArray()
        {
            return array;
        }
    }
}
