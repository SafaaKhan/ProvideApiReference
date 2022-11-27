﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_Models.Helpers
{
    public class ResponseModel
    {
        public bool IsSeccuss { get; set; }
        public object Value { get; set; }
        public string DisplayMessage { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }//??

        public static ResponseModel Seccuss(object obj, string displayMessage) => new ResponseModel { IsSeccuss=true, Value = obj, DisplayMessage= displayMessage, ErrorMessage="",StatusCode=200};
        public static ResponseModel Failure (string error, int statusCode) => new ResponseModel { IsSeccuss=false, Value = null, DisplayMessage="", ErrorMessage=error,StatusCode= statusCode };
    }
}
