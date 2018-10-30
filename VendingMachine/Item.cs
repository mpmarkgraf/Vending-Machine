using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    /// <summary>
    /// Hey yall
    /// </summary>
    public abstract class Item
    {
        #region properties

        /// <summary>
        /// properties inherited by each item type subclass for
        /// slot ID, item name, item price, inventory quantity,
        /// and quantity sold during each transaction event
        /// </summary>
        public string Slot { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }
        public decimal Price { get; }
        public int Qty { get; private set; } = 5;
        public int QtySold { get; private set; } = 0;
        public string DisplayQty
        {
            get
            {
                if (Qty > 0)
                {
                    return Qty.ToString();
                }
                else
                {
                    return "Sold Out";
                }
            }
        }

        #endregion

        #region constructor

        /// <summary>
        /// constructor for Item objects, slot ID, 
        /// item name, and price are passed in as
        /// arguments
        /// </summary>
        /// <param name="slot">slot ID</param>
        /// <param name="name">item name</param>
        /// <param name="price">item price</param>
        public Item(string slot, string name, decimal price)
        {
            Slot = slot;
            Name = name;
            Price = price;
        }

        #endregion

        #region methods

        /// <summary>
        /// decrements inventory quantity by 1,
        /// and increments quantity sold by 1
        /// </summary>
        public void QtyUpdate()
        {
            Qty--;
            QtySold++;
        }

        public Item Clone()
        {
            Item result = null;

            if (this is Gum)
            {
                result = new Gum(Slot, Name, Price);
            }
            else if(this is Candy)
            {
                result = new Candy(Slot, Name, Price);
            }
            else if (this is Chip)
            {
                result = new Chip(Slot, Name, Price);
            }
            else if (this is Drink)
            {
                result = new Drink(Slot, Name, Price);
            }

            result.Qty = Qty;
            result.QtySold = QtySold;

            return result;
        }

        /// <summary>
        /// abstract method to return a string sound
        /// </summary>
        /// <returns></returns>
        public abstract string MakeSound();
        /// <summary>
        /// abstract method to return a string for ascii art
        /// </summary>
        /// <returns></returns>
        public abstract string ItemArt();

        #endregion
    }
}
