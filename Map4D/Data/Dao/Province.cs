using CommonLogger.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map4D.Data.Dao
{
    public class Province:AdoHelper
    {
        AdoHelper helper = null;
        public Province()
        {
            string connection = AdoHelper.ConnectionString;
            helper = new AdoHelper(connection);
        }
    }
}