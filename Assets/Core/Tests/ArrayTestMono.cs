using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JYCore.Runtime;
using System;

public class ArrayTestMono : MonoBehaviour
{
    [Serializable]
    public class TestClass
    {
        public int a;
        public double b;
        public string c;
    }
    public int array2dTest = 3;
    public Serializable2DArray<int> valueTest;
    public Serializable2DArray<double> valueTest1;
    public Serializable2DArray<TestClass> valueTest2;   

    public void Reset()
    {
        //add test data int test field
        valueTest = new Serializable2DArray<int>(3, 3);
        valueTest[0, 0] = 1;
        valueTest[0, 1] = 2;
        valueTest[0, 2] = 3;
        valueTest[1, 0] = 4;
        valueTest[1, 1] = 5;
        valueTest[1, 2] = 6;
        valueTest[2, 0] = 7;
        valueTest[2, 1] = 8;
        valueTest[2, 2] = 9;

        //add test data double test field
        valueTest1 = new Serializable2DArray<double>(3, 3);
        valueTest1[0, 0] = 1.1;
        valueTest1[0, 1] = 2.2;
        valueTest1[0, 2] = 3.3;
        valueTest1[1, 0] = 4.4;
        valueTest1[1, 1] = 5.5;
        valueTest1[1, 2] = 6.6;
        valueTest1[2, 0] = 7.7;
        valueTest1[2, 1] = 8.8;
        valueTest1[2, 2] = 9.9;



        //add test data class test field
        valueTest2 = new Serializable2DArray<TestClass>(3, 3);
        valueTest2[0, 0] = new TestClass() { a = 1, b = 1.1, c = "1" };
        valueTest2[0, 1] = new TestClass() { a = 2, b = 2.2, c = "2" };
        valueTest2[0, 2] = new TestClass() { a = 3, b = 3.3, c = "3" };
        valueTest2[1, 0] = new TestClass() { a = 4, b = 4.4, c = "4" };
        valueTest2[1, 1] = new TestClass() { a = 5, b = 5.5, c = "5" };
        valueTest2[1, 2] = new TestClass() { a = 6, b = 6.6, c = "6" };
        valueTest2[2, 0] = new TestClass() { a = 7, b = 7.7, c = "7" };
        valueTest2[2, 1] = new TestClass() { a = 8, b = 8.8, c = "8" };
        valueTest2[2, 2]= new TestClass() { a = 9, b = 9.9, c = "9" };
    }
}
