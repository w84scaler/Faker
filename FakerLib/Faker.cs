using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading;

namespace FakerLib
{
    public class Faker
    {
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t)
        {
            var ctors = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).OrderByDescending(ctor => ctor.GetParameters().Length).ToList();
            object obj = null;
            foreach (var ctor in ctors)
            {
                var ctorParams = ctor.GetParameters();
                List<object> ctorValues = new List<object>();
                foreach (var ctorparam in ctorParams)
                {
                    object ctorValue = GetDefaultValue(ctorparam.ParameterType);
                    if (ctorValue == null)
                    {
                        ctorValue = Create(ctorparam.ParameterType);
                    }
                    ctorValues.Add(ctorValue);
                }
                try
                {
                    obj = ctor.Invoke(ctorValues.ToArray());
                    break;
                } 
                catch
                {
                    continue;
                }
            }
            return obj;
        }

        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }
    }
}
