using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LasteUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LasteUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract().
                Requires().
                IsGreaterOrEqualsThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura" ));

            _payments.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            LasteUpdateDate = DateTime.Now;
        }

        public void Inactivate()
        {
            Active = false;
            LasteUpdateDate = DateTime.Now;
        }

    }
}
