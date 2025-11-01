using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BE.Domain.DTOs.Users
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
        public DateTime Expiration { get; set; }
    }
}
