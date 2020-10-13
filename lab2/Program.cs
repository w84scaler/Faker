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
        public bool alive;
        public string name;
        public MedCard medcard;
        public BusinessCard businesscard;
        public DateTime birth;

        public int age { get; set; }

        private Person(string _name, bool _alive)
        {
            name = _name;
            alive = _alive;
        }
        
        public Person(string _name, int _age, bool _alive)
        {
            name = _name;
            alive = _alive;
            age = _age;
            throw new Exception();
        }
    }

    public struct MedCard
    {
        public double t;
        public Person person;
        public int id;

        public MedCard(int _id, Person _person, double _t)
        {
            t = 36 + _t;
            person = _person;
            id = _id;
        }
    }

    public struct BusinessCard
    {
        public int id;
        public string company;
        public Person person;
    }
}
