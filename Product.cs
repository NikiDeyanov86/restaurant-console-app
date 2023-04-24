using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_OOP_137knz
{
    internal static class ProductCategory
    {
        public const String SALAD = "салата";
        public const String SOUP = "супа";
        public const String MAIN_DISH = "основно";
        public const String DESERT = "десерт";
        public const String DRINK = "напитка";
    }
    public abstract class Product
    {
        protected String m_name;
        protected decimal m_price;
        protected int m_quantity;

        public Product(String name, decimal price, int quantity)
        {
            m_name = name;
            m_price = price;
            m_quantity = quantity;
        }

        public Product(String name) { 
            var new_product = Restaurant.Instance.GetProductByName(name);
            if (new_product != null)
            {
                m_name = new_product.m_name;
                m_price = new_product.m_price;
                m_quantity = new_product.m_quantity;
            }
            
        }

        public Product(Product product)
        {
            m_name = product.m_name;
            m_price = product.m_price;
            m_quantity = product.m_quantity;
        }

        public abstract void ProductInfo();

        public String getName()
        {
            return m_name;
        }
        public void setName(String new_name)
        {
            m_name = new_name;
        }
        public decimal getPrice()
        {
            return m_price;
        }
        public void setPrice(decimal new_price)
        {
            m_price = new_price;
        }
    }
}
