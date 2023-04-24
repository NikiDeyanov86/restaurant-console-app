using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            state = ProgramState.RUNNING;

            while(state == ProgramState.RUNNING)
            {
                Console.Write(">");
                Console.Out.Flush();
                parseCommand(Encoding.UTF8.GetString(Encoding.Default.GetBytes(Console.ReadLine())));
            }
        }

        public static void parseCommand(String cmd)
        {
            if(cmd != null)
            {
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
                            Console.WriteLine(category);
                            String name = splitCmd[1];
                            int quantity = Int32.Parse(splitCmd[2]);
                            decimal price = Decimal.Parse(splitCmd[3]);

                            restaurant.AddProductToMenu(category, name, quantity, price);
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
