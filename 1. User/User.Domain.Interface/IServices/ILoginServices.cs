using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO.Token;
using User.Domain.Business.DTO;

namespace User.Domain.Interface.IServices
{
    public interface ILoginServices
    {
        TokenResponse CreateToken(UserOwnerDTO user);
    }
}
