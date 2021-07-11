using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entity;

namespace WebApi.Application.Interfaces
{
    public interface ICardRepository
    {
        Task InsertCardAsync(Card card);
        Task<Card> FindCardAsynbc(int id);
    }
}
