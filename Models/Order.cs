using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Models
{
    internal class Order
    {
        public uint id { get; set; }
        public DateTime OrderDate => DateTime.Now.Date;
        public string Description { get; set; }
        public float OrderPrice { get; set; }
        public DateTime CloseDate => DateTime.Now.Date;

        [ForeignKey("Client")]
        public uint ClientID { get; set; }


    }
}
