using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.CreateToken;

namespace WebApi.Application.Interfaces
{
    public interface ICreateToken
    {
        long CreatTokenAsync(TokenCreate request);
    }
}
