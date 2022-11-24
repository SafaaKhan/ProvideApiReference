using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_Models.DTOs
{
    public class ResponseDto
    {
        public bool IsSeccuss { get; set; } = true;
        public object Result { get; set; } 
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }= new List<string>();
    }
}
