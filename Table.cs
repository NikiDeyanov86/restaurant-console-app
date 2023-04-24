using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project_OOP_137knz
{
    internal class Table
    {
        private short m_id;
        private Queue<Product> m_order = new Queue<Product>();
        private bool m_isBusy;

        public Table(short id)
        {
            m_id = id;
            m_isBusy = false;
        }

        public void MakeOrder(List<Product> products)
        {
            if (!m_isBusy)
            {
                m_isBusy = true;
            }
            products.ForEach(product => m_order.Enqueue(product));
        }

        public short GetId()
        {
            return m_id;
        }
        public bool IsBusy()
        {
            return m_isBusy;
        }
    }
}
