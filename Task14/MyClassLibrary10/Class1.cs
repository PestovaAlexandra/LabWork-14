using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary10
{
    public class Person
    {

        protected string name;
        protected int age;

        protected static Random rnd = new Random();
        static string[] NameMale = { "Иван", "Пётр", "Максим", "Андрей", "Сергей", "Данил", "Григорий", "Алексей", "Артём", "Евгений" };
        static string[] SurnameMale = { "Сидоров", "Петров", "Иванов", "Фёдоровых", "Постнов", "Осипов", "Корзников", "Бутин", "Утробин", "Байбурин" };
        static string[] NameFemale = { "Жанна", "Светлана", "Елена", "Ольга", "Анжелика", "Юлия", "Анна", "Анастасия", "Ирина", "Александра" };
        static string[] SurnameFemale = { "Аристова", "Горожанина", "Мазилова", "Бахарева", "Кувыркина", "Пильгун", "Мальцева", "Попова", "Шапикаева", "Ионникова" };
        static string[] Qualification = { "высшая", "первая", "вторая" };
        static string MakeName()
        {
            if (rnd.Next(0, 2) == 0)
            {
                return NameFemale[rnd.Next(NameFemale.Length)] +" "+ SurnameFemale[rnd.Next(SurnameFemale.Length)];
            }
            else
                return NameMale[rnd.Next(NameMale.Length)] +" "+ SurnameMale[rnd.Next(SurnameMale.Length)];
        }
        public Person()
        {
            name = MakeName();
            age = rnd.Next(18,99);
        }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public override string ToString()
        {
            return Name.ToString() + " " + Age.ToString();
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"Имя: {name}\nВозраст: {age}");
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class Employee : Person, IComparable, ICloneable,IEquatable<Object>//служащий
    {
        protected int experience;//опыт работы
        public Employee() : base()
        {
            experience = rnd.Next(1,70);
        }
        public Employee(string name, int age, int experience)
            : base(name, age)
        {
            this.experience = experience;
        }
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }
        public override string ToString()
        {
            return base.ToString() +" стаж: " + Experience;
        }
        public override void PrintInfo()
        {

            Console.WriteLine($"Имя: {name}\nВозраст: {age}\nСтаж работы: {experience}");
        }
        public int CompareTo(Object obj)//реализация интерфейса
        {
            Employee employeeToCompare = (Employee)obj;
            if (this.experience > employeeToCompare.experience) return 1;
            if (this.experience < employeeToCompare.experience) return -1;
            return 0;
        }
        public object Clone()
        {
            return new Employee("Клон "+ this.name,this.age, this.experience);
        }
        public Employee ShallowCopy() //поверхностное копирование
        {
            return (Employee)this.MemberwiseClone();
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override bool Equals(Object obj)
        {
            try
            {
                Employee g = (Employee)obj;
                return (g.Name == this.Name && g.Age == this.Age && g.Experience == this.Experience);
            }
            catch
            {
                return false;
            }


        }
    }
    public class Worker : Employee//рабочий
    {
        private string workPlace;//название предприятия
        static string[] Factors = { "КамКабель", "БИО Мед", "Завод ГСК", "Лесозавод", "КамГЭС", "Пороховой завод", "НПО Искра", "БумКомбинат", "ПНОС", "ОАО Мотавилихинские заводы" };

        static string MakeFactor()
        {
            return Factors[rnd.Next(Factors.Length)];
        }
        public Worker() : base()
        {
            workPlace =MakeFactor() ;
        }
        public Worker(string name, int age, int experience, string workPlace)
            : base(name,age, experience)
        {
            this.workPlace = workPlace;
        }
        public string WorkPlace
        {
            get { return workPlace; }
            set { workPlace = value; }
        }
        public override string ToString()
        {
            return base.ToString() + " место работы: " + WorkPlace ;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Место работы: {workPlace}");
        }
        public Worker ShallowCopy() //поверхностное копирование
        {
            return (Worker)this.MemberwiseClone();
        }
        public object Clone()
        {
            return new Worker("Клон " + this.name, this.age, this.experience, this.workPlace);
        }
        public int CompareTo(object obj)//реализация интерфейса
        {
            Worker workerToCompare = (Worker)obj;
            if (this.experience > workerToCompare.experience) return 1;
            if (this.experience < workerToCompare.experience) return -1;
            return 0;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
    public class Engneer : Employee//инженер
    {
        private string qualification;//квалификация
        static string[] Qualifications = { "высшая", "первая", "вторая" };
        static string MakeQualification()
        {
            return Qualifications[rnd.Next(Qualifications.Length)];
        }
        public Engneer() : base()
        {
            qualification =MakeQualification() ;
        }
        public Engneer(string name, int age, int experience, string qualification)
            : base(name, age, experience)
        {
            this.qualification = qualification;
        }
        public string Qualification
        {
            get { return qualification; }
            set { qualification = value; }
        }
        public override string ToString()
        {
            return base.ToString() + " квалификация: " + Qualification;
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Квалификация: {qualification}");
        }
        public int CompareTo(object obj)//реализация интерфейса
        {
            Engneer engeneerToCompare = (Engneer)obj;
            if (this.experience > engeneerToCompare.experience) return 1;
            if (this.experience < engeneerToCompare.experience) return -1;
            return 0;
        }
        public object Clone()
        {
            return new Engneer("Клон " + this.name, this.age, this.experience, this.qualification);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
