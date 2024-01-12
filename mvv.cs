using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace AddMongo
{
    //mvv表(也可以是华严魔方通用表)
    internal class mvv
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string space { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string owner { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string company_id { get; set; }
        public List<string> company_ids { get; set; }

        // 构造函数，设置默认值
        public mvv(string id, string name1, string namedata,string space1,string owner1)
        {
            _id = id;
            name = namedata;
            space = space1;
            created = DateTime.Now;
            modified = DateTime.Now;
            owner = owner1;
            created_by = owner1;
            modified_by = owner1;
            company_id = space1;
            company_ids = new List<string> { space1 };
            
        }
    }

}
