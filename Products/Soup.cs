using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_OOP_137knz.Products
{
    internal class Soup : Product
    {
        public Soup(String name, decimal price, int quantity) : base(name, price, quantity) { }

        public override void ProductInfo()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Информация за продукт: " + m_name);
            Console.WriteLine("Грамаж: " + m_quantity);
        }
    }
}
