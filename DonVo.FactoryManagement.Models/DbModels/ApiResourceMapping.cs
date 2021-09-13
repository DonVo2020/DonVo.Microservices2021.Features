using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DonVo.FactoryManagement.Models.DbModels
{
    public class ApiResourceMapping
    {
        public string Controller { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
        public int Id  { get; set; }
    }
}
