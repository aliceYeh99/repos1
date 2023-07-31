// See https://aka.ms/new-console-template for more information
using GettingProperties;
using System.Reflection.PortableExecutable;

Example e =  new Example();
List<Person> list = new List<Person>();
Person person = new Person();

list.Add(person);
e.ReadList(list);
e.ReportTable();

Console.WriteLine("Hello, World!");