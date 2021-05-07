using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AREA1.Models {
    public class UserModel {

        public string user_id { get; set; }
        public string author { get; set; }
        public string birthday { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }//hash
        public string name { get; set; }
    }
}
