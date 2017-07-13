using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibData.Bussiness
{
    public class ValidateUser
    {
        public static bool CheckUser(string username, string password)
        {
            try
            {
                if(username.Equals(ConfigType.VietinbankUser) && password.Equals(ConfigType.VietinbankPassword))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
