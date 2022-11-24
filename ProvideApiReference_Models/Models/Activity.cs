using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvideApiReference_Models.Models
{
    public class Activity
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }//??
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;

    }
}
