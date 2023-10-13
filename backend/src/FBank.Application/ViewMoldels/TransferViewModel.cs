using FBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBank.Application.ViewMoldels
{
    public class TransferViewModel
    {
        public int AccountNumberFrom { get; set; }           
        public int AccountNumberTo { get; set; }
        public decimal Value { get; set; }
    }
}
