using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABProject.Events;

namespace ABProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IMyEvent ev = new BankAccountCreatedEvent();
            Console.WriteLine(ev.GetType());


            Console.ReadKey();
        }


        public static void PrintEvent<T>(IMyEvent ev)
        {
            Console.WriteLine(typeof(T));

        }



    }
