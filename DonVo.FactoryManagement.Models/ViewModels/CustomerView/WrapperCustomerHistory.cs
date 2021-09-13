﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.FactoryManagement.Models.ViewModels.CustomerView
{
   public class WrapperCustomerHistory
    {
        public WrapperCustomerHistory() {
            ListOfData = new List<CustomerHistory>();
        }
        public long TotalRecords { get; set; }

        public List<CustomerHistory> ListOfData { get; set; }
    }
}
