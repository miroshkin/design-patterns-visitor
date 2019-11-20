using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DesignPatterns_Visitor
{
    //original source https://metanit.com/sharp/patterns/3.11.php

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Design Patterns - Visitor!");
            Bank bank = new Bank();
            bank.Add(new Company(){Name="IBM", Number = "123456", RegistrationNumber = "ibm_2345"});
            bank.Add(new Person(){Name = "Anton Miroshkin", Number = "5555"});

            HtmlVisitor htmlVisitor = new HtmlVisitor();
            XmlVisitor xmlVisitor = new XmlVisitor();

            var htmlResult = bank.Accept(htmlVisitor);
            htmlResult.ForEach(r => Console.WriteLine(r));

            Console.WriteLine();

            var xmlResult = bank.Accept(xmlVisitor);
            xmlResult.ForEach(x => Console.WriteLine(x));
        }
    }

    public class Bank
    {
        List<IAccount> accounts = new List<IAccount>();

        public void Add(IAccount account)
        {
            accounts.Add(account);
        }

        public void Remove(IAccount account)
        {
            accounts.Remove(account);
        }

        public List<string> Accept(IVisitor visitor)
        {
            var result = new List<string>();
            foreach (var account in accounts)
            {
                 result.Add(account.Accept(visitor));
            }

            return result;
        }

        

        
    }

    public interface IAccount
    {
        string Accept(IVisitor visitor);
    }

    public interface IVisitor
    {
        string VisitCompanyAccount(Company company);
        string VisitPersonAccount(Person person);
    }

    public class Person : IAccount
    {
        public string Name { get; set; }
        public string Number { get; set; }

        public string Accept(IVisitor visitor)
        {
            return visitor.VisitPersonAccount(this);
        }
    }

    public class Company : IAccount
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Number { get; set; }

        public string Accept(IVisitor visitor)
        {
            return visitor.VisitCompanyAccount(this);
        }
    }

    public class HtmlVisitor : IVisitor
    {
        public string VisitCompanyAccount(Company company)
        {
            return $"<h1>{company.Name}</h1><div>{company.Number}</div><p>{company.RegistrationNumber}</p>";
        }

        public string VisitPersonAccount(Person person)
        {
            return $"<h1>{person.Name}</h1><div>{person.Number}</div>";

        }
    }

    public class XmlVisitor : IVisitor
    {
        public string VisitCompanyAccount(Company company)
        {
            return $"<Company><Name>{company.Name}</Name><Number>{company.Number}</Number><RegistrationNumber>{company.RegistrationNumber}</RegistrationNumber></Company>";

        }

        public string VisitPersonAccount(Person person)
        {
            return $"<Person><Name>{person.Name}</Name><Number>{person.Number}</Number></Person>";

        }
    }
}
