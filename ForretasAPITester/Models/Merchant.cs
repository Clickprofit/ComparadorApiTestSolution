using System;
namespace ForretasAPITester.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Utilizador { get; set; }
        public string PalavraPasse { get; set; }

        public Merchant()
        {
        }
    }
}
