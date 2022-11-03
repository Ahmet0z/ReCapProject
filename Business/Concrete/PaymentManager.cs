using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Business;

namespace Business.Concrete
{
    namespace Bussiness.Concrete
    {
        public class PaymentManager : IPaymentService
        {
            IPaymentDal _paymentDal;
            ICardService _cardService;

            public PaymentManager(IPaymentDal paymentDal, ICardService cardService)
            {
                _paymentDal = paymentDal;
                _cardService = cardService;

            }

            [SecuredOperation("user,admin")]
            public IResult Add(Payment payment)
            {
                var result = BusinessRules.Run(CheckCardExist(payment.CreditCardNumber, payment.ExpirationDate,
                    payment.SecurityCode));
                if (result != null)
                {
                    return new ErrorResult(result.Message);
                }

                _paymentDal.Add(payment);
                return new SuccessResult();
            }

            [SecuredOperation("admin")]
            public IResult Delete(Payment payment)
            {
                _paymentDal.Delete(payment);
                return new SuccessResult();
            }

            [SecuredOperation("admin")]
            public IDataResult<Payment> Get(Payment payment)
            {
                return new SuccessDataResult<Payment>(_paymentDal.Get(x => x.PaymentId == payment.PaymentId));
            }

            [SecuredOperation("admin")]
            public IDataResult<List<Payment>> GetAll()
            {
                return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
            }

            [SecuredOperation("admin")]
            public IDataResult<Payment> GetByPaymentId(int paymentId)
            {
                var result = _paymentDal.Get(x => x.PaymentId == paymentId);
                if (result == null)
                {
                    return new ErrorDataResult<Payment>(Messages.PaymentNotFound);
                }

                return new SuccessDataResult<Payment>(result);
            }

            [SecuredOperation("admin")]
            public IResult Update(Payment payment)
            {
                _paymentDal.Update(payment);
                return new SuccessResult();
            }

            //Business Rules

            private IResult CheckCardExist(string cardNumber, string expiration, string securityCode)
            {
                var result = _cardService.GetbyCardNumber(cardNumber);
                if (result.Data.ExpirationDate == expiration && result.Data.SecurityCode == securityCode)
                {
                    return new SuccessResult();
                }

                return new ErrorResult(Messages.cardNotFound);
            }
        }
    }
}
