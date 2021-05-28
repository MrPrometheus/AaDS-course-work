using System;
using System.Xml.Linq;

namespace Cursovik
{
    public class Department : IDisposable
    {
        public int Number { get; private set; }
        public string Profile { get; private set; }

        public Department(int number, string profile)
        {
            Number = number;
            Profile = profile;
        }

        public override string ToString()
        {
            return $"Номер отделения - {Number}, Профиль отделения - {Profile}";
        }

        public void Dispose()
        {
            Number = default;
            Profile = default;
        }
        
        public XElement WriteXml()
        {
            XElement department = new XElement("Department");
            department.Add(new XAttribute("number", Number));
            department.Add(new XAttribute("profile", Profile));
            return department;
        }
    }
}