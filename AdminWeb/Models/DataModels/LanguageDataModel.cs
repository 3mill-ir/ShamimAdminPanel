﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminWeb.Models.DataModels
{
    public class LanguageDataModel
    {
        public string Language { get; set; }
        public string Image { get; set; }
        public Nullable<bool> isRTL { get; set; }
        public string LanguageName { get; set; }
        public Nullable<double> Weight { get; set; }
    }
}