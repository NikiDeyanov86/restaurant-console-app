using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_OOP_137knz.Products
{
    internal class Drink : Product
    {
        protected double m_calories;

        public Drink(String name, decimal price, int quantity) : base(name, price, quantity)
        {
            m_calories = quantity * 1.5;
        }

        public override void ProductInfo()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Информация за продукт: " + m_name);
            Console.WriteLine("Милилитри: " + m_quantity);
            Console.WriteLine("Калории: " + m_calories);
        }
    }
}
