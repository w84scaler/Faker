using System;
using System.Collections.Generic;

using FakerLib;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker faker = new Faker();
            german obj = faker.Create<german>();
        }
    }

    public struct StructExample
    {
        public StructExample(int d, string a)
        {
            this.d = d;
            this.a = "yes";
        }

        public int d;
        public string a;
    }

    public class Example2
    {
        public string name { get; set; }
        private int num;
        public int numa { get; set; }
        public double doubleItem { get; set; }
        private Example2(string name, int num, double doubleItem, int numa)
        {
            this.name = name;
            this.num = 13;
            this.numa = 42;
            this.doubleItem = 56.23;
        }
        public Example2(string name)
        {
            this.name = name;
        }
    }

    public class Example
    {
        public List<int> ListIntItem { get; set; }
        public List<string> ListStringItem;
        public List<AssosiationExample> ListAssosItem { get; set; }
        public string StringItem { get; set; }
        public int IntNumber { get; set; }
        public double DoubleNumber { get; set; }
        public long LongNumber;
        public DateTime DateTimeItem;
        public Example2 Example2Item;
    }

    public class AssosiationExample
    {
        public Example DTOItem;
        public int HigherInt { get; set; }
    }

    public class AnotherAssosiationExample
    {
        public AssosiationExample AnotherDTOItem;
    }

    public class german
    {
        public bool pososal;
        public german()
        {
            pososal = false;
        }

        private german(bool b)
        {
            pososal = b;
        }
    }
}
