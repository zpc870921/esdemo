using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo.Models
{
    public abstract class Document
    {
        public JoinField Join { get; set; }
    }

    public abstract class MyDocument
    {
        public JoinField MyJoinField { get; set; }
        public int Id { get; set; }
    }

    public class MyParent:MyDocument
    {
        [Text]
        public string ParentProperty { get; set; }
    }

    public class MyChild : MyDocument
    {
        [Text]
        public string ChildProperty { get; set; }
    }


    public class Parent
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string IgnoreMe { get; set; }
    }

    public class Child:Parent
    {
        
    }

    public class Company
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class Employee
    {
        public string LastName { get; set; }
        public int Salary { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsManager { get; set; }
        public List<Employee> Employees { get; set; }
        public TimeSpan Hours { get; set; }
    }

    public class ParentWithStringId
    {
        public new string Id { get; set; }
        public string Description { get; set; }
        public string IgnoreMe { get; set; }
    }

    public class A
    {
        public A Child { get; set; }
    }
}
