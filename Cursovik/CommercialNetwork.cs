using System;
using System.Text;
using System.Xml.Linq;

namespace Cursovik
{
    public class CommercialNetwork : IDisposable
    {
        public string CommercialNetworkName { get; private set; }
        public Shop[] Shops { get; private set; }

        private int _count;
        private const int _maxLength = 10;
        
        public CommercialNetwork(string name, int length = _maxLength)
        {
            CommercialNetworkName = name;
            Shops = new Shop[length];
        }
 
        public bool IsEmpty => _count == 0;
        
        public int Count => _count;

        public Shop Peek() => Shops[_count - 1];

        public bool PushShop(string shopName)
        {
            if(_count == Shops.Length)
                throw new InvalidOperationException("Стек заполнен, добавление невозможно!");
            if(SearchShop(shopName, out int index)) 
                throw new InvalidOperationException("Магазин с таким именем уже существует. Наименование магазина должно быть уникальным.");
           
            Shops[_count++] = new Shop(shopName);
            return true;
        }

        public Shop PopShop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст!");
            
            Shop item = Shops[--_count];
            Shops[_count] = default(Shop);

            return item;
        }

        public void AddDepartment(string toShop, int number, string profile)
        {
            if(!SearchShop(toShop, out int index)) throw new InvalidOperationException("Невозможно добавить отделение. Магазина не существует.");
            Shops[index].AddDepartment(number, profile);
        }

        public void RemoveDepartment(string fromShop, int number)
        {
            if(!SearchShop(fromShop, out int index)) throw new InvalidOperationException("Невозможно удалить отделение. Магазина не существует.");
            Shops[index].RemoveDepartment(number);
        }

        public bool SearchShop(string name, out int index)
        {
            for (int i = 0; i < _count; i++)
            {
                if (Shops[i].ShopName == name)
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            b.Append($"Наименование тороговой сети - {CommercialNetworkName}\n");
            for (int i = 0; i < _count; i++)
            {
                b.Append("   ");
                b.Append(Shops[i].ToString());
            }

            return b.ToString();
        }

        public string getShopInfo(string shop)
        {
            if(!SearchShop(shop, out int index)) throw new InvalidOperationException("Магазин не существует.");
            return Shops[index].ToString();
        }

        public Department GetDepartament(string shop, int number)
        {
            if(!SearchShop(shop, out int index)) throw new InvalidOperationException("Невозможно найти отделение. Магазина не существует.");
            return Shops[index].GetDepartament(number);
        }

        public bool SearchDepartmentFirst(string shop, int number)
        {
            if(!SearchShop(shop, out int index)) throw new InvalidOperationException("Невозможно найти отделение. Магазина не существует.");
            return Shops[index].ContainsFirst(number);
        }
        
        public bool SearchDepartmentEnd(string shop, int number)
        {
            if(!SearchShop(shop, out int index)) throw new InvalidOperationException("Невозможно найти отделение. Магазина не существует.");
            return Shops[index].ContainsEnd(number);
        }
        
        public void Dispose()
        {
            CommercialNetworkName = default;
            for (int i = 0; i < _count; i++)
            {
                Shops[i].Dispose();
                Shops[i] = default;
            }

            Shops = default;
            _count = default;
        }

        public XElement WriteXml()
        {
            XElement network = new XElement("CommercialNetwork");
            network.Add(new XAttribute("name", CommercialNetworkName));
            for (int i = 0; i < _count; i++)
            {
                network.Add(Shops[i].WriteXml());
            }
            return network;
        }
    }
}