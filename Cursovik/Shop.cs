using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Cursovik
{
    public class Node
    {
        public Node(Department department)
        {
            Data = department;
        }
        public Department Data { get; set; }
        public Node Previous { get; set; }
        public Node Next { get; set; }
    }
    public class Shop : IDisposable, IEnumerable<Department>
    {
        public string ShopName { get; private set; }

        private Node _head;
        private Node _tail;
        private int _count;

        public Shop(string name)
        {
            ShopName = name;
        }

        public int Count => _count;
        
        public bool IsEmpty => _count == 0;
        
        public void AddDepartment(int number, string profile)
        {
            Node node = new Node(new Department(number, profile));

            if (_head == null)
            {
                _head = node;
                _tail = node;
            }
            else
            {
                Node current = _head;
 
                while (current != null)
                {
                    if (current.Data.Number > number) break;
                    current = current.Next;
                }

                if (current == null)
                {
                    _tail.Next = node;
                    node.Previous = _tail;
                    _tail = node;
                }
                else
                {
                    if (current == _head)
                    {
                        Node temp = _head;
                        node.Next = temp;
                        _head = node;
                        temp.Previous = node;
                    }
                    else
                    {
                        node.Previous = current.Previous;
                        node.Next = current;
                        current.Previous.Next = node;
                        current.Previous = node;
                    }
                }
            }
            
            _count++;
        }
        
        public bool RemoveDepartment(int number)
        {
            Node current = _head;
 
            while (current != null)
            {
                if (current.Data.Number.Equals(number)) break;
                current = current.Next;
            }

            if (current == null) return false;
            
            if(current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
                _tail = current.Previous;
            }
 
            if(current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }
            else
            {
                _head = current.Next;
            }
            _count--;
            
            current = default;
            
            return true;
        }

        public bool ContainsFirst(int number)
        {
            Node current = _head;
            while (current != null)
            {
                if (current.Data.Number.Equals(number)) return true;
                current = current.Next;
            }
            return false;
        }
        
        public bool ContainsEnd(int number)
        {
            Node current = _tail;
            while (current != null)
            {
                if (current.Data.Number.Equals(number)) return true;
                current = current.Previous;
            }
            return false;
        }

        public Department GetDepartament(int number)
        {
            Node current = _head;
            while (current != null)
            {
                if (current.Data.Number.Equals(number)) return current.Data;
                current = current.Next;
            }
            throw new Exception("Отделения не существует");
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            b.Append($"Наименование Магазина - {ShopName}:\n");
            int i = 0;
            foreach (var item in this)
            {
                b.Append($" {i} Отделение) {item.ToString()} \n");
                i++;
            }

            return b.ToString();
        }

        public void Dispose()
        {
            ShopName = default;
            foreach (var item in this)
            {
                item.Dispose();
            }

            _head = null;
            _tail = null;
            _count = default;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
        
        IEnumerator<Department> IEnumerable<Department>.GetEnumerator()
        {
            Node current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
 
        public IEnumerable<Department> BackEnumerator()
        {
            Node current = _tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }
}