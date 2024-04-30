using Alfasoft.Models;

namespace Alfasoft.SecurityToken
{
    public class TokenReturnModel
    {
        public User User { get; set; }
        public string Token { get; set; }
        public string PrimerioAcesso { get; set; }
        public string Situacao { get; set; }
    }
}
