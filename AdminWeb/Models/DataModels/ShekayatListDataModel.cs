using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class ShekayatListDataModel
    {
        public ShekayatListDataModel()
        {
            Shekayats = new List<Shekayat>();
        }
        public List<Shekayat> Shekayats { get; set; }
        public int Total { get; set; }
    }
}