using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICardService
    {
        IResult Add(Card card);
        IResult Update(Card card);
        IResult Delete(Card card);
        IDataResult<List<Card>> GetAllCards();
        IDataResult<Card> GetByCustomerId(int customerId);
        IDataResult<Card> GetbyCardNumber(string cardNumber);

        IDataResult<List<Card>> GetCardListByCustomerId(int customerId);
    }
}
