﻿namespace SharedTrip.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;

        public const int UserMinUsername = 4;
        public const int UserMinPassword = 5;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const int MinStartPointValue = 1;
        public const int MaxStartPointValue = 30;
        public const int MinEndPointValue = 1;
        public const int MaxEndPointValue = 30;
        public const int MinSeatsValue = 2;
        public const int MaxSeatsValue = 6;
    }
}