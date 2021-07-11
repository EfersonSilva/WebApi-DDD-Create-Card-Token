using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.Command;

namespace WebApi.Application.Interfaces
{
    public interface ICardApplication
    {
        Task<CardResponse> SaveCardAsync(CardRequest cardRequest);
    }
}
