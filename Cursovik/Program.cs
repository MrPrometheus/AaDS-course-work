using System;

namespace Cursovik
{
    internal class Program
    {
        private static CommercialNetwork _network = null;
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
                                Console.WriteLine("Выход из кFоманды"); 
                                break; 
                            default: 
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
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода, выход из команды");
                    }
                    Console.WriteLine("Сеть магазинов создана"); 
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
                            Console.WriteLine(e);
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
                        if (int.TryParse(dNumber, out int number))
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
                                Console.WriteLine(e);
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
                case "REMTC":
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
                            Console.WriteLine(e);
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
                                var s = Console.ReadLine();
                                Console.WriteLine("Введите название магазина:");
                                Console.WriteLine($"Отделение {_network.GetDepartament(s, number)} удалено");
                                _network.RemoveDepartment(s, number);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
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
                                Console.WriteLine(_network.SearchDepartmentFirst(s, num) ? $"В магазине {s} найдено отделение {_network.GetDepartament(s, num).ToString()}" : "Товар не найден");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
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
                            Console.WriteLine(e);
                            Console.WriteLine("Выход из команды");
                        }
                        break;
                    } 
                    Console.WriteLine("Сеть магазинов отсутсвует"); 
                    break; 
                case "RF":
                    /*if (_network != null)
                    {
                        if (!_network.IsEmpty)
                        {
                            Console.WriteLine("Текущие данные будут удалены! Продолжить? Y/N");
                            var answer = Console.ReadLine();
                            if (string.Equals(answer, "Y"))
                            {
                                _network.Dispose(); 
                                readFile();
                            }
                        }
                        else
                        {
                            readFile();
                        } 
                        break;
                    } 
                    Console.WriteLine("Сначала создайте торговую сеть");*/
                    break; 
                case "WRF":
                    /*if (_network != null)
                    {
                        _network.writeFile(); 
                        break;
                    } 
                    Console.WriteLine("Сначала создайте Сеть магазинов");*/
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
        
        
    }
}