﻿using System;
using System.Reflection;

namespace Custom_Attributes
{
    //[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)] // (1)
    [AttributeUsage(AttributeTargets.Field |
                    AttributeTargets.Class, AllowMultiple = true)] // (2)
    class MyAttribute : Attribute
    {
        //private String _msg;
        public String Msg { get; private set; }

        private int _val;
        //public int Val { get; private set; }
        public int Val
        {
            get { return _val; }
            set { _val = value; }
        }
        public MyAttribute() {}
        public MyAttribute(object o)
        {
        }
        public MyAttribute(Type t)
        {
        }
        public MyAttribute(String m)
        {
            Msg = m;
        }
    }
    // Aplicação do custom attribute MyAttribute - Ver PDF "Notas sobre Reflexao"
    [My("Ola")] 
    class Test
    {
        [My(typeof(Math))]
        [My(Math.PI)]
        [My("Ola", Val = 7)]
        int f1;

        [My("Ola")]
        int f2;
        
		[My(Val = 8)]
        int f3;
        
        [My("Ola", Val = 5)]
        [My("Adeus")]
        int f4;
    }

    internal class Example
    {
        private static void Main1(string[] args)
        {
            Type tTest = typeof(Test);
            FieldInfo f = tTest.GetField("f1", BindingFlags.NonPublic | 
                                         BindingFlags.Instance);
            //FieldInfo f = tTest.GetField("f3", BindingFlags.NonPublic | BindingFlags.Instance);
            
            Console.WriteLine(f);
            if (Attribute.IsDefined(f, typeof (MyAttribute)))
				Console.WriteLine("{0} is defined", typeof (MyAttribute));
            else
				Console.WriteLine("{0} is not defined", typeof (MyAttribute));

            // Generate a custom attribute object
            MyAttribute[] atts = (MyAttribute[])f.GetCustomAttributes(
                typeof(MyAttribute), false); // Segundo parametro: bool inherit
            // inherit = true to search this member's inheritance chain to find the attributes 
            // (but the attribute must be defined AttributeUsage(..., Inherit = true)); otherwise, false. 
			// This parameter is ignored for properties and events.
            //if (atts != null)
            //{
            //    Console.WriteLine("Attribute info:");
            //    Console.WriteLine("Val = {0}", atts[0].Val);
            //    Console.WriteLine("Msg = {0}", atts[0].Msg);
            //}
            foreach (MyAttribute att in atts) {
                Console.WriteLine("Val = {0}", att.Val);
                Console.WriteLine("Msg = {0}", att.Msg);

                att.Val = 10; // Atualiza info do atributo na metadata?
            }
        }
        //
        // VER DEFINIÇAO DO ATRIBUTO NA METADATA
        //
    }
}
