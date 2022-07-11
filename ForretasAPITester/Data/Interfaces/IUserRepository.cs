using System;
using ForretasAPITester.Models;

namespace ForretasAPITester.Data.Interfaces
{
    public interface IUserRepository
    {
        public Merchant GetUser(LoginData loginData);
    }
}
