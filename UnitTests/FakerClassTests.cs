using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using FakerLib;

namespace UnitTests
{
    [TestClass]
    public class FakerClassTests
    {
        Faker faker;

        [TestInitialize]
        public void Setup()
        {
            faker = new Faker();
        }

        [TestMethod]
        public void Test_Structure_with_ctor()
        {
            var obj = faker.Create<MedCard>();
            Assert.IsTrue(obj.t >= 36 && obj.t <= 37, "wrong t");
            Assert.AreNotEqual(obj.person, GetDefaultValue(obj.person.GetType()), "null person");
            Assert.AreNotEqual(obj.id, GetDefaultValue(obj.id.GetType()), "wrong id");
        }

        [TestMethod]
        public void Test_Structure_without_ctor()
        {
            var obj = faker.Create<BusinessCard>();
            Assert.AreNotEqual(obj.id, GetDefaultValue(obj.id.GetType()), "wrong id");
            Assert.AreNotEqual(obj.company, GetDefaultValue(obj.company.GetType()), "null company name");
            Assert.AreNotEqual(obj.person, GetDefaultValue(obj.person.GetType()), "null person");
        }

        [TestMethod]
        public void Test_Exeption_in_ctor()
        {
            var obj = faker.Create<Person>();
            Assert.IsTrue(obj.name == "bruh", "wrong name");
        }

        [TestMethod]
        public void Test_Circular_references_dodging()
        {
            var obj = faker.Create<Person>();
            Assert.IsNull(obj.medcard.person, "person not null");
        }

        [TestMethod]
        public void Test_Circular_references_dodging_in_List()
        {
            var obj = faker.Create<BusinessCard>();
            Assert.IsTrue(obj.person.businesscard.Count == 0, "List<> isnt empty");
        }

        [TestMethod]
        public void Test_ListGeneration()
        {
            var obj = faker.Create<List<List<Person>>>();
            Assert.IsTrue(obj.GetType().IsGenericType);
            Assert.IsTrue(obj[0].GetType().IsGenericType, "List<List<T>> generation fail");
            Assert.IsTrue(obj[0][0].businesscard.GetType().IsGenericType, "inner List<struct> generation fail");
        }

        [TestMethod]
        public void Test_Generators_Values()
        {
            var obj = faker.Create<TestClass>();
            Assert.AreNotEqual(obj.boolv, GetDefaultValue(obj.boolv.GetType()), "BoolGenerator fail");
            Assert.AreNotEqual(obj.intv, GetDefaultValue(obj.intv.GetType()), "IntGenerator fail");
            Assert.AreNotEqual(obj.doublev, GetDefaultValue(obj.doublev.GetType()), "DoubleGenerator fail");
            Assert.AreNotEqual(obj.stringv, GetDefaultValue(obj.stringv.GetType()), "StringGenerator fail");
            Assert.AreNotEqual(obj.datetimev, GetDefaultValue(obj.datetimev.GetType()), "DateTimeGenerator fail");
        }

        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }

        public class TestClass
        {
            public int intv;
            public bool boolv;
            public double doublev;
            public string stringv;
            public DateTime datetimev;
        }

        public class Person
        {
            public bool alive;
            public string name;
            public MedCard medcard;
            public List<BusinessCard> businesscard;
            public DateTime birth;

            public int age { get; set; }

            public Person()
            {

            }

            private Person(string _name, bool _alive)
            {
                name = "bruh";
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
}
