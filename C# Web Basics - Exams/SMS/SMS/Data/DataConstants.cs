using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;

        public const int ProductMaxLength = 20;

        public const int ProductNameMinLength = 4;
        public const int ProductNameMaxLength = 20;
        public const decimal PriceMinValue = 0.05m;
        public const decimal PriceMaxValue = 1000m;

        public const int UserMinUsername = 4;
        public const int UserMinPassword = 5;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

    }
}
