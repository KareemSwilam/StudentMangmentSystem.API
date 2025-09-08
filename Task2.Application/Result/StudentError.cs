using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Application.Result
{
    public static class StudentError
    {
        public static CustomError AlreadyExist(string email)
            => new CustomError("Student Already Exist", $"Student With Email  {email} Already Exist");
    }
}
