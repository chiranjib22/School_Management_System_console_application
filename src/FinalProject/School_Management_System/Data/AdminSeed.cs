using School_Management_System.Models;
using School_Management_System.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Management_System.Data
{
    public static class AdminSeed
    {
        public static Admin[] GetAdmin()
        {
            return [
                new Admin
                {
                    Id = -2,
                    UserName = "admin",
                    Password = "$2a$11$oOCLQLJaIJYCI/uRPa82jeBBwWy5TMg54UVp2eEFNlIhhCbP6J.qS",
                    Name = "Super Admin",
                },
                ];
        }
    }
}
