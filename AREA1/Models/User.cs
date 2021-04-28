using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AREA1.Models
{
    public class USER
    {
        public string user_id { get; set; }
        public string author { get; set; }
        public string birthday { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }//hash
    }

    public class SBJECT
    {
        public int sbject_id { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
        public string year { get; set; }
        public string semester { get; set; }
        public int point { get; set; }

        [DataType(DataType.Date)]
        public DateTime time { get; set; }
    }
}
