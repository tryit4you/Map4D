using CommonLogger.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map4D.Data.DAO
{
    public class ProvinceDao:AdoHelper
    {
        AdoHelper helper = null;
        public ProvinceDao()
        {
            string connection = AdoHelper.ConnectionString;
            helper = new AdoHelper(connection);
        }
    }
}