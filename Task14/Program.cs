using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary10;

namespace Task14
{
    class Program
    {
        public static void EmployeeExp(List<List<Person>> list)
        {
            //Имена служащих со стажем не менее заданного
            int exp = 40;
            Console.WriteLine($"\nИмена служащих со стажем не менее заданного: {exp}\n");

            var countByExp = from g in list
                             from p in g
                             where p is Employee && ((Employee)p).Experience >= exp
                             select p.Name;

            //var countByExp = list.SelectMany(g => g).Select(p => p).Where(p => p is Employee && ((Employee)p).Experience >= exp).OrderBy(p=>p.Name);

            foreach (var item in countByExp)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public static void countOnPlant(List<List<Person>> list)
        {
            //Количество рабочих на заводе
            string plant = "Пороховой завод";
            Console.WriteLine($"\nКоличество рабочих на заводе: {plant}\n");

            var countWorkers = (from g in list
                                from p in g
                                where p is Worker && ((Worker)p).WorkPlace == plant
                                select p).Count();

            //var countWorkers = list.SelectMany(g => g).Select(p => p).Where(p => p is Worker && ((Worker)p).WorkPlace == plant).Count();
            Console.WriteLine($"\nКоличество рабочих на заводе {plant}:  {countWorkers}\n");
        }
        public static void Different(List<List<Person>> list)
        {
            //Разность множеств Employee||Worker||Engneer and Eployee
            Console.WriteLine($"\nРазность множеств Employee||Worker||Engneer and Eployee\n");

            var personDiff = (from g in list
                              from p in g
                                  //where p is Engneer
                              select p).Except(from g2 in list
                                               from p2 in g2
                                               where p2 is Worker
                                               select p2);

            //var personDiff = list.SelectMany(g => g).Select(p => p).Except(list.SelectMany(g2 => g2).Select(p2 => p2).Where(p2 => p2 is Worker));
            foreach (var item in personDiff)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public static void MiddleAge(List<List<Person>> list)
        {
            //Средний возраст рабочих
            var middleAge = (from g in list
                             from p in g
                             where p is Worker
                             select p.Age).Average();

            //var middleAge = list.SelectMany(g => g).Select(p => p).Average(p => p.Age);
            Console.WriteLine($"\nСредний возраст рабочих {Math.Round( middleAge,2)}\n");
        }
        public static void Groupping(List<List<Person>> list)
        {
            //Группировка рабочик по заводу
            var groups = from g in list
                         from p in g
                         orderby p.Age
                         group p by p.Age;
            //var groups = list.SelectMany(g => g).Select(p => p).OrderBy(p => p.Age).GroupBy(p => p.Age);
            foreach(IGrouping<int, Person> g in groups)
            {
                Console.WriteLine(g.Key);
                    foreach (var e in g)
                        Console.WriteLine(e);
            }
        }

        static void Main(string[] args)
        {
            List<Person> factory = new List<Person>();
            List<Person> workshop = new List<Person>();

            List<List<Person>> list = new List<List<Person>>();

            int n = 6;
            for(int i=0;i<n;i++)
            {
                if (i % 2 == 0)
                {
                    factory.Add(new Employee());
                    workshop.Add(new Employee());
                }
                if (i % 3 == 0)
                {
                    factory.Add(new Worker());
                    workshop.Add(new Worker());
                }
                else
                {
                    factory.Add(new Engneer());
                    workshop.Add(new Engneer());
                }
            }

            foreach (Person item in factory)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            foreach (Person item in workshop)
            {
                Console.WriteLine(item.ToString());
            }

            list.Add(factory);
            list.Add(workshop);

            //Имена служащих со стажем не менее заданного
            EmployeeExp(list);

            //Количество рабочих на заводе
            countOnPlant(list);

            //Разность множеств Engneer and Eployee
            Different(list);

            //Средний возраст рабочих
            MiddleAge(list);

            //Группировка рабочик по заводу
            Groupping(list);

            Console.ReadKey();
        }
    }
}
