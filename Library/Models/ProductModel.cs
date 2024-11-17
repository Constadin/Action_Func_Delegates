using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class ProductModel
    { 
        public Guid Id { get; private set; }
        public string ItemName { get; private set; }
        public decimal ItemPrice { get; private set; }

        // Setter method for Id
        public void SetGuidId(Guid id)
        {
            this.Id = id;
        }

        // Setter method for ItemName
        public void SetItemName(string itemName)
        {
            this.ItemName = itemName;
        }

        // Setter method for ItemPrice
        public void SetItemPrice(decimal itemPrice)
        {
            this.ItemPrice = itemPrice;
        }
    }

}
