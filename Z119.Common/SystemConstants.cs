using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.Common
{
    public static class SystemConstants
    {
        public static int pageIndex = 1;
        public static int pageSize = 10;

        public static string NotBorrow = "Chưa mượn";
        public static string Borrowed = "Đã mượn";
    }

    public class CurrentPersion
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Unit { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }

}
