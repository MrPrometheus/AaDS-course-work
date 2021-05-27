using System;

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
    }
}