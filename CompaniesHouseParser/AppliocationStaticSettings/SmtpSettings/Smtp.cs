﻿namespace CompaniesHouseParser.Settings
{
    public class Smtp : ISmtp
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
