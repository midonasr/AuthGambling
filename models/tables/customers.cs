using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthGambling.models
{
    public class customers
    {
        [Key]
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Boolean marketingConsent { get; set; } = false;
  

    }
}
