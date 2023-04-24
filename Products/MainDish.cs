using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_OOP_137knz.Products
{
    internal class MainDish : Product
    {
        protected double m_calories;

        public MainDish(String name, decimal price, int quantity) : base(name, price, quantity)
        {
            m_calories = quantity;
        }

        public override void ProductInfo()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Информация за продукт: " + m_name);
            Console.WriteLine("Грамаж: " + m_quantity);
            Console.WriteLine("Калории: " + m_calories);
        }
    }
}
