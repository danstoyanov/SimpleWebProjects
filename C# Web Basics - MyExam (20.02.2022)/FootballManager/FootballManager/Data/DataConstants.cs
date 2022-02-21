using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;
        public const int EmailMaxLength = 60;
        public const int EmailMinLength = 10;

        public const int FullNameMaxLength = 80;
        public const int FullNameMinLength = 5;
        public const int PositionMaxLength = 20;
        public const byte MaxSpeed = 10;
        public const byte MinSpeed = 1;
        public const byte MaxEndurance = 10;
        public const byte MinEndurance = 1;
        public const int PlayerDescriptinMaxLength = 200;

        public const int UserMinUsername = 4;
        public const int UserMinPassword = 5;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    }
}