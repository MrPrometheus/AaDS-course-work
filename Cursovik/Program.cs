using System;
using System.Xml.Linq;

namespace Cursovik
{
    internal class Program
    {
        private static CommercialNetwork _network = null;
        private static XDocument xdoc;
        public static void Main(string[] args)
        {
            Help();
            var command = Console.ReadLine();
            while (true)
            {
                if (command.Equals("EXT"))
                {
                    Console.WriteLine("Данные автоматически не сохранятся. Вы уверены, что хотите выйти? Y/N"); 
                    var answer = Console.ReadLine();
                    if (string.Equals(answer, "Y"))
                    {
                        _network.Dispose();
                        _network = null; 
                        break;
                    }
                    else
                    {
                        command = Console.ReadLine();
                    }
                }
                commandRecognizer(command);
                command = Console.ReadLine();
            }
        }
        
        private static void commandRecognizer(string command) { 
            switch (command) { 
                case "h": 
                    Help();
                    break; 
                case "ADDCN":
                    if (_network != null)
                    {
                        Console.WriteLine("Текущие данные будут удалены! Продолжить? Y/N");
                        switch (Console.ReadLine())
                        {
                            case "Y": 
                                _network.Dispose(); 
                                _network = null; 
                                Console.WriteLine("Введите название сети:");
                                _network = new CommercialNetwork(Console.ReadLine()); 
                                Console.WriteLine("Сеть магазинов создана"); 
                                break; 
                            case "N": 
                                Console.WriteLine("Выход из команды"); 
                                break; 
                            default: 
                                Console.WriteLine("Сеть магазинов НЕ создана");
                                Console.WriteLine("Ввод неверен, выход из команды");
                                break;
                        } 
                        break;
                    } 
                    Console.WriteLine("Введите название сети:");
                    var nameNetwork = Console.ReadLine();
                    Console.WriteLine("Введите количество магазинов в сети (длина стека):");
                    if (int.TryParse(Console.ReadLine(), out int n))
                    {
                        _network = new CommercialNetwork(nameNetwork, n); 
                        Console.WriteLine("Сеть магазинов создана"); 
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода, выход из команды");
                    }
                    break; 
                case "ADDS":
                    if (_network != null)
                    {
                        Console.WriteLine("Введите название магазина:"); 
                        var sName = Console.ReadLine();
                        try
                        {
                            if (_network.PushShop(sName))
                            {
                                Console.WriteLine("Маазин добавлен");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Маазин НЕ добавлен");
                            Console.WriteLine("Выход из команды");
                        }
                        break;
                    } 
                    Console.WriteLine("Сеть магазинов отсутствует"); 
                    break; 
                case "ADDD": 
                    if (_network != null)
                    {
                        Console.WriteLine("Введите номер отделения:"); 
                        var dNumber = Console.ReadLine(); 
                        Console.WriteLine("Введите профиль отделения:"); 
                        var dProfile = Console.ReadLine();
                        if (int.TryParse(dNumber, out int number) && number >= 0)
                        {
                            try
                            {
                                Console.WriteLine("Введите наименование магазина");
                                var ss = Console.ReadLine();
                                _network.AddDepartment(ss, number, dProfile);
                                Console.WriteLine($"Отделение с номером {dNumber} добавлено в магазин {ss}");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Выход из команды");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Номер отделения должен быть больше 0 и быть числом");
                            Console.WriteLine("Ввод неверен, выход из команды");
                        }
                        break; 
                    }
                    Console.WriteLine("Сеть магазинов отсутсвует"); 
                    break;
                case "REMCN":
                    if (_network != null)
                    {
                        _network.Dispose(); 
                        _network = null; 
                        Console.WriteLine("Данные удалены"); 
                        break;
                    }
                    Console.WriteLine("Сеть магазинов отсутсвует");
                    break; 
                case "REMS":
                    if (_network != null)
                    {
                        try
                        {
                            Console.WriteLine($"Магазин {_network.PopShop().ShopName} удален"); 
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Выход из команды");
                        }
                        break;
                    } 
                    Console.WriteLine("Сеть магазинов отсутсвует");
                    break;
                case "REMD":
                    if (_network != null)
                    { 
                        Console.WriteLine("Введите номер отделения:"); 
                        var dNumber = Console.ReadLine(); 
                        
                        if (int.TryParse(dNumber, out int number)) 
                        {
                            try
                            {
                                Console.WriteLine("Введите название магазина:");
                                var s = Console.ReadLine();
                                Console.WriteLine($"{_network.GetDepartament(s, number)} удалено из магазина {s}");
                                _network.RemoveDepartment(s, number);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Выход из команды");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ввод неверен, выход из команды");
                        } 
                        break; 
                    } 
                    Console.WriteLine("Сеть магазинов отсутсвует"); 
                    break; 
                case "FS":
                    if (_network != null)
                    {
                        Console.WriteLine("Введите наименование магазина:");
                        var sName = Console.ReadLine();
                        Console.WriteLine(_network.SearchShop(sName, out int index) ? $"Магазин {sName} найден" : "Магазин не найден"); 
                        break;
                    } 
                    Console.WriteLine("Сеть магазинов отсутсвует"); 
                    break; 
                case "FD":
                    if (_network != null)
                    {
                        Console.WriteLine("Введите номер отделения:"); 
                        var dNumber = Console.ReadLine();
                        
                        if (int.TryParse(dNumber, out int num))
                        {
                            try
                            {
                                Console.WriteLine("Введите наименование магазина:");
                                var s = Console.ReadLine();
                                Console.WriteLine(_network.SearchDepartmentFirst(s, num) ? $"В магазине {s} найдено отделение {_network.GetDepartament(s, num).ToString()}" : "Отделение не найдено");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Выход из команды");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ввод неверен, выход из команды");
                        }
                        break;
                    } 
                    Console.WriteLine("Сеть магазинов отсутсвует");
                    break; 
                case "SHS":
                    if (_network != null)
                    {
                        Console.WriteLine("Введите наименование магазина:");
                        var nnn = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(_network.getShopInfo(nnn));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Выход из команды");
                        }
                        break;
                    } 
                    Console.WriteLine("Сеть магазинов отсутсвует"); 
                    break; 
                case "RF":
                    try
                    {
                        if (_network != null)
                        {
                            if (!_network.IsEmpty)
                            {
                                Console.WriteLine("Текущие данные будут удалены! Продолжить? Y/N");
                                var answer = Console.ReadLine();
                                if (string.Equals(answer, "Y"))
                                {
                                    _network.Dispose();
                                    _network = null;
                                    ReadXml();
                                }
                                Console.WriteLine("Данные не прочтианы.");
                            }
                            else
                            {
                                ReadXml();
                            } 
                        }
                        else
                        {
                            ReadXml();
                        }
                    }
                    catch (Exception e)
                    {
                        _network.Dispose();
                        _network = null;
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Выход из команды");
                    }
                    break; 
                case "WRF":
                    if (_network != null)
                    {
                        xdoc = new XDocument();
                        xdoc.Add(_network.WriteXml()); 
                        xdoc.Save("network.xml");
                        Console.WriteLine("Структура выгружена в network.xml");
                        break;
                    } 
                    Console.WriteLine("Сначала создайте Сеть магазинов");
                    break; 
                case "SH":
                    if (_network != null)
                    {
                        Console.WriteLine(_network.ToString());
                        break;
                    } 
                    Console.WriteLine("Сначала создайте Сеть магазинов"); 
                    break;
                default: 
                    Console.WriteLine("Нет такой команды"); 
                    break; 
            }
        }

        private static void Help()
        {
            Console.WriteLine("h вывести данную подсказку"); 
            Console.WriteLine("ADDCN добавить торговую сеть"); 
            Console.WriteLine("ADDS добавить магазин");
            Console.WriteLine("ADDD добавить отдел"); 
            Console.WriteLine("REMCN удалить торговую сеть"); 
            Console.WriteLine("REMS удалить магазин");
            Console.WriteLine("REMD удалить отдел"); 
            Console.WriteLine("FS найти магазин"); 
            Console.WriteLine("FD найти департамент"); 
            Console.WriteLine("SHD Вывести информацию о магазине"); 
            Console.WriteLine("SH вывести всю структуру"); 
            Console.WriteLine("RF заполнить структуры данными из файла"); 
            Console.WriteLine("WRF переписать файл текущими данными"); 
            Console.WriteLine("EXT выход");
        }

        private static void ReadXml()
        {
            XDocument xdoc = XDocument.Load("input.xml");
            XElement networkElement = xdoc.Element("CommercialNetwork");
            if(networkElement == null) 
                throw new Exception("Ошибка формата данных. В файле должен быть корневой элемент CommercialNetwork");
            
            XAttribute networkNAme = networkElement.Attribute("name");
            if(networkNAme == null || String.IsNullOrWhiteSpace(networkNAme.Value)) 
                throw new Exception("Ошибка формата данных. У сети магазинов должен быть атрибут name и должен быть не пустым!");
            
            _network = new CommercialNetwork(networkNAme.Value);
            foreach (XElement shop in networkElement.Elements("Shop"))
            {
                XAttribute shopName = shop.Attribute("name");
                if (shopName == null || String.IsNullOrWhiteSpace(shopName.Value))
                    throw new Exception("Ошибка формата данных. У магазина должен быть атрибут name и должен быть не пустым!");
                _network.PushShop(shopName.Value);
                
                foreach (var department in shop.Elements("Department"))
                {
                    XAttribute dNumber = department.Attribute("number");
                    XAttribute dProfile = department.Attribute("profile");
                    
                    if(dNumber == null || String.IsNullOrWhiteSpace(dNumber.Value)) 
                        throw new Exception("Ошибка формата данных. У отделения должен быть атрибут number и должен быть уникальным числом больше 0!");
                    if(dProfile == null || String.IsNullOrWhiteSpace(dProfile.Value)) 
                        throw new Exception("Ошибка формата данных. У отделения должен быть атрибут profile и должен быть не пустой строкой!");

                    if (int.TryParse(dNumber.Value, out int index))
                    {
                        _network.AddDepartment(shopName.Value, index, dProfile.Value);
                    }
                    else
                    {
                        throw new Exception("Ошибка формата данных. У отделения атрибут number должен быть уникальным числом больше 0!");
                    }
                }
            }
            Console.WriteLine("Структура загружена");
        }
    }
}