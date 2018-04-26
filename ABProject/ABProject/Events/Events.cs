using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABProject.Events
{
    public interface IMyEvent
    {

    }

    public class BankAccountCreatedEvent : IMyEvent
    {
        public MyAccountNumberGuid AccountId { get; set; }
        public string AccountHolderName { get; set; }
    }

    public class MoneyAddedEvent : IMyEvent
    {
        public MyAccountNumberGuid AccountId { get; set; }
        public decimal Amount { get; set; }
    }

    public class MoneyTakenEvent : IMyEvent
    {
        public MyAccountNumberGuid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
    


}
