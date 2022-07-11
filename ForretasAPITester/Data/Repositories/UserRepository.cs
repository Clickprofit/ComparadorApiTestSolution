using System;
using ForretasAPITester.Data.Interfaces;
using ForretasAPITester.Data.Repositories.Abstract;
using ForretasAPITester.Helpers;
using ForretasAPITester.Models;
using Microsoft.Extensions.Configuration;

namespace ForretasAPITester.Data.Repositories
{
    public class UserRepository : AbstractRepository<Merchant>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public Merchant GetUser(LoginData loginData)
        {
            string hashedPassword = (new SecurityHelper()).EncryptText(loginData.Password);
            string query = $"SELECT TOP 1 * FROM Merchant WHERE Utilizador = '{loginData.Username}' " +
                $"AND PalavraPasse = '{hashedPassword}'";

            return GetSingleMappedEntity(query, null);
        }
    }
}
