using Course_project_OOP_137knz.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_OOP_137knz
{   
    
    /* Representing the restaurant - Singleton pattern */
    public sealed class Restaurant
    {
        private static readonly Restaurant m_instance = new Restaurant();
        private static List<Table> m_tables;
        private static Dictionary<String, List<Product>> m_productMenu;

        private static decimal m_totalPrice = 0;
        private static int m_totalProductsSalesCount = 0;
        private static Dictionary<String, int> m_totalProductSalesCountPerCategory;
        private static Dictionary<String, decimal> m_totalProductSalesPerCategory;
        
        private Restaurant()
        {
            m_tables = new List<Table>();
            m_productMenu = new Dictionary<String, List<Product>>();
            m_productMenu.Add(ProductCategory.SALAD, new List<Product>());
            m_productMenu.Add(ProductCategory.SOUP, new List<Product>());
            m_productMenu.Add(ProductCategory.MAIN_DISH, new List<Product>());
            m_productMenu.Add(ProductCategory.DESERT, new List<Product>());
            m_productMenu.Add(ProductCategory.DRINK, new List<Product>());
            m_totalProductSalesCountPerCategory = new Dictionary<String, int>();
            m_totalProductSalesPerCategory = new Dictionary<String, decimal>();
            /* Initialize the tables */
            for (short i = 1; i <= 30; i++)
            {   
                m_tables.Add(new Table(i));
            }

            /* Initialize the dicts */
            m_totalProductSalesCountPerCategory.Add(ProductCategory.SALAD, 0);
            m_totalProductSalesPerCategory.Add(ProductCategory.SALAD, 0);
            m_totalProductSalesCountPerCategory.Add(ProductCategory.SOUP, 0);
            m_totalProductSalesPerCategory.Add(ProductCategory.SOUP, 0);
            m_totalProductSalesCountPerCategory.Add(ProductCategory.MAIN_DISH, 0);
            m_totalProductSalesPerCategory.Add(ProductCategory.MAIN_DISH, 0);
            m_totalProductSalesCountPerCategory.Add(ProductCategory.DESERT, 0);
            m_totalProductSalesPerCategory.Add(ProductCategory.DESERT, 0);
            m_totalProductSalesCountPerCategory.Add(ProductCategory.DRINK, 0);
            m_totalProductSalesPerCategory.Add(ProductCategory.DRINK, 0);
        }
        public static Restaurant Instance
        {
            get
            {
                return m_instance;
            }
        }

        public Dictionary<String, List<Product>> getMenuInstance()
        {
            return m_productMenu;
        }

        public void AddProductToMenu(String category, String name, int quantity, decimal price)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (!IsProductAvailable(name))
            {   
                if (category.Contains(ProductCategory.SALAD))
                {
                    m_productMenu[ProductCategory.SALAD].Add(new Salad(name, price, quantity));
                }
                else if(category.Contains(ProductCategory.SOUP))
                {
                    m_productMenu[ProductCategory.SOUP].Add(new Soup(name, price, quantity));
                }
                else if (category.Contains(ProductCategory.MAIN_DISH))
                {
                    m_productMenu[ProductCategory.MAIN_DISH].Add(new MainDish(name, price, quantity));
                }
                else if (category.Contains(ProductCategory.DESERT))
                {
                    m_productMenu[ProductCategory.DESERT].Add(new Desert(name, price, quantity));
                }
                else if (category.Contains(ProductCategory.DRINK))
                {
                    m_productMenu[ProductCategory.DRINK].Add(new Drink(name, price, quantity));
                }
                else
                {
                    Console.WriteLine(String.Format("Продуктът {0} не може да бъде добавен в менюто, " +
                                                    "поради неправилна категория {1}", name, category));
                }
                /*
                switch (category)
                {
                    case ProductCategory.SALAD:
                        m_productMenu[ProductCategory.SALAD].Add(new Salad(name, price, quantity));
                        break;
                    case ProductCategory.SOUP:
                        m_productMenu[ProductCategory.SOUP].Add(new Soup(name, price, quantity));
                        break;
                    case ProductCategory.MAIN_DISH:
                        m_productMenu[ProductCategory.MAIN_DISH].Add(new MainDish(name, price, quantity));
                        break;
                    case ProductCategory.DESERT:
                        m_productMenu[ProductCategory.DESERT].Add(new Desert(name, price, quantity));
                        break;
                    case ProductCategory.DRINK:
                        m_productMenu[ProductCategory.DRINK].Add(new Drink(name, price, quantity));
                        break;
                    default:
                        Console.WriteLine(String.Format("Продуктът {0} не може да бъде добавен в менюто, " +
                                                        "поради неправилна категория {1}", name, category));
                        break;
                } */
            } else
            {
                Console.WriteLine(String.Format("Продуктът {0} е вече наличен в менюто", name));
            }
        }

        public void TakeOrder(short id, List<String> productsStr)
        {
            List<Product> orderList = new List<Product>();
            if((1 <= id) && (id <= 30))
            {   
                foreach (var item in productsStr)
                {   
                    if(IsProductAvailable(item))
                    {
                        orderList.Add(GetProductByName(item));
                    }
                    
                }
                m_tables.ElementAt(id).MakeOrder(orderList);
                foreach(var product in orderList)
                {   
                    m_totalPrice += product.getPrice();
                    m_totalProductsSalesCount++;
                    switch(product.GetType().Name.ToString())
                    {
                        case "Salad":
                            m_totalProductSalesPerCategory[ProductCategory.SALAD] += product.getPrice();
                            m_totalProductSalesCountPerCategory[ProductCategory.SALAD]++;
                            break;
                        case "Soup":
                            m_totalProductSalesPerCategory[ProductCategory.SOUP] += product.getPrice();
                            m_totalProductSalesCountPerCategory[ProductCategory.SOUP]++;
                            break;
                        case "MainDish":
                            m_totalProductSalesPerCategory[ProductCategory.MAIN_DISH] += product.getPrice();
                            m_totalProductSalesCountPerCategory[ProductCategory.MAIN_DISH]++;
                            break;
                        case "Desert":
                            m_totalProductSalesPerCategory[ProductCategory.DESERT] += product.getPrice();
                            m_totalProductSalesCountPerCategory[ProductCategory.DESERT]++;
                            break;
                        case "Drink":
                            m_totalProductSalesPerCategory[ProductCategory.DRINK] += product.getPrice();
                            m_totalProductSalesCountPerCategory[ProductCategory.DRINK]++;
                            break;
                    }
                }
            }
        }

        public void infoSales()
        {
            Console.OutputEncoding = Encoding.UTF8;
            short takenTables = 0;
            foreach(var table in m_tables)
            {
                if(table.IsBusy())
                {
                    takenTables++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Общо заети маси през деня: " + takenTables);
            Console.WriteLine(String.Format("Общо продажби: {0} - {1}", m_totalProductsSalesCount, m_totalPrice));
            Console.WriteLine("По категории:");
            Console.WriteLine(String.Format(" - Салата: {0} - {1}", m_totalProductSalesCountPerCategory[ProductCategory.SALAD],
                                                                    m_totalProductSalesPerCategory[ProductCategory.SALAD]));
            Console.WriteLine(String.Format(" - Супа: {0} - {1}", m_totalProductSalesCountPerCategory[ProductCategory.SOUP],
                                                                    m_totalProductSalesPerCategory[ProductCategory.SOUP]));
            Console.WriteLine(String.Format(" - Основно ястие: {0} - {1}", m_totalProductSalesCountPerCategory[ProductCategory.MAIN_DISH],
                                                                    m_totalProductSalesPerCategory[ProductCategory.MAIN_DISH]));
            Console.WriteLine(String.Format(" - Десерт: {0} - {1}", m_totalProductSalesCountPerCategory[ProductCategory.DESERT],
                                                                    m_totalProductSalesPerCategory[ProductCategory.DESERT]));
            Console.WriteLine(String.Format(" - Напитка: {0} - {1}", m_totalProductSalesCountPerCategory[ProductCategory.DRINK],
                                                                    m_totalProductSalesPerCategory[ProductCategory.DRINK]));
        }

        public bool IsProductAvailable(String name)
        {
            foreach(var item in m_productMenu)
            {
                foreach(var product in item.Value)
                {
                    if(product.getName().Equals(name))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Product GetProductByName(String name)
        {
            foreach (var item in m_productMenu)
            {
                foreach (var product in item.Value)
                {
                    if (product.getName().Equals(name))
                    {
                        return product;
                    }
                }
            }

            return null;
        }
    }
}
