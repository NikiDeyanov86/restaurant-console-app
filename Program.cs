using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Course_project_OOP_137knz
{   
    
    internal class Program
    {   
        enum ProgramState
        {
            NOT_STARTED = 0,
            RUNNING = 1,
            STOPPED = 2
        }

        internal static class QuickCommandType
        {
            public const String INFO = "инфо";
            public const String SALES = "продажби";
            public const String EXIT = "изход";
        }

        private static ProgramState state = ProgramState.NOT_STARTED;
        private static Restaurant restaurant = Restaurant.Instance;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            state = ProgramState.RUNNING;

            while(state == ProgramState.RUNNING)
            {
                Console.Write(">");
                Console.Out.Flush();
                parseCommand(Console.ReadLine());
            }
        }

        public static void parseCommand(String cmd)
        {
            if(cmd != null)
            {
                Console.WriteLine(cmd);
                List<String> splitCmd = cmd.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();
                if (cmd.Contains(QuickCommandType.INFO) && (splitCmd.Count == 2))
                {
                    String product_name = splitCmd[1];
                    if (restaurant.IsProductAvailable(product_name))
                    {
                        restaurant.GetProductByName(product_name).ProductInfo();
                    }
                    else
                    {
                        Console.WriteLine($"Продуктът {product_name} не е наличен в менюто!");
                    }
                }
                else if (cmd.Contains(QuickCommandType.SALES) && (splitCmd.Count == 1))
                {
                    restaurant.infoSales();
                }
                else if (cmd.Contains(QuickCommandType.EXIT))
                {
                    restaurant.infoSales();
                    Console.WriteLine("Затваряне след 10 секунди...");
                    Thread.Sleep(10000);
                    state = ProgramState.STOPPED;
                    return;
                }
                else
                {
                    if (restaurant.getMenuInstance().Count <= 0)
                    {
                        if (splitCmd.Count == 4)
                        {
                            String category = splitCmd[0];
                            String name = splitCmd[1];
                            int quantity = 0;
                            decimal price = decimal.Parse(splitCmd[3].Trim(), CultureInfo.InvariantCulture);
                            Console.WriteLine(price);
                            if (Int32.TryParse(splitCmd[2], out quantity))
                            {
                                Console.WriteLine(price);
                                restaurant.AddProductToMenu(category, name, quantity, price);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неправилен формат на добавяне на продукт!");
                        }
                    }
                    else
                    {
                        if (splitCmd.Count > 1)
                        {
                            short table_id = short.Parse(splitCmd[0]);
                            if ((1 <= table_id) && (table_id <= 30))
                            {
                                splitCmd.RemoveAt(0);
                                List<String> orderList = new List<String>(splitCmd);
                                restaurant.TakeOrder(table_id, orderList);
                            }
                            else
                            {
                                Console.WriteLine("Невалиден номер на маса (0-30)!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Невалиден формат на поръчка!");
                        }
                    }
                }
            }
        }
    }
}
