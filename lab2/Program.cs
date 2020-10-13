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
            Person obj = faker.Create<Person>();
            Console.ReadLine();
        }
    }

    public class Person
    {
        public bool _pososal;
        public string _name;
        public BusinessCard _card1;
        public BusinessCard _card2;
        public int _age { get; set; }
        public Person()
        {
            _pososal = false;
        }

        private Person(bool pososal)
        {
            _pososal = pososal;
        }

        private Person(string name, bool pososal)
        {
            _name = name;
            _pososal = pososal;
        }
        
        public Person(string name, int age, bool pososal)
        {
            _name = name;
            _pososal = pososal;
            _age = age;
            throw new Exception();
        }
    }

    public class BusinessCard
    {
        public Person _person;
        public int id;
    }
}
