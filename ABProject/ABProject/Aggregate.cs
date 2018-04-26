using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABProject.Events;

namespace ABProject
{
    class Aggregate
    {
        public abstract MyAccountNumberGuid Id { get; }
        private List<IMyEvent> _unsubmittedEventsList;
        private Dictionary<Type, Action<IMyEvent>> _callbacks;  //Key = type of event, Value = Aggregate's method with a param = event


        public List<IMyEvent> GetUnsubmittedEvents()
        {
            var tmp = _unsubmittedEventsList;
            _unsubmittedEventsList.Clear();
            return tmp;
        }


        protected void RaiseEvent(IMyEvent ev)
        {
            //add this event to _unsubmittedEventsList
            _unsubmittedEventsList.Add(ev);

            //perform corresponding operation
            var operation = _callbacks[ev.GetType()];
            operation.Invoke(ev);

        }

        protected void Register<T>(Action<T> operation) // where T : IMyEvent
        {
            _callbacks[typeof(T)] = (x => operation((T)x));
        }


    }


    class BankAccount : Aggregate
    {
        //backing field for Id property
        private MyAccountNumberGuid _id;

        public override MyAccountNumberGuid Id => _id; //getter

        public string AccountHolderName { get; set; }


        private BankAccount()
        {
            // call Register() to store all types of operations we need to perform on each type of event
            Register<BankAccountCreatedEvent>(OnBankAccountCreatedEvent);
            Register<MoneyAddedEvent>(OnMoneyAddedEvent);
            Register<MoneyTakenEvent>(OnMoneyTakenEvent);
        }


        public static BankAccount Create(MyAccountNumberGuid id, string accountHolderName)
        {
            var acc = new BankAccount();
            
            acc.RaiseEvent(new BankAccountCreatedEvent {AccountId = id, AccountHolderName = accountHolderName });

            return acc;
        }


        public void OnBankAccountCreatedEvent(BankAccountCreatedEvent ev)
        {
            _id = ev.AccountId;
            AccountHolderName = ev.AccountHolderName;
            Console.WriteLine("bank account created: " + ev.AccountId);
        }

        public void OnMoneyAddedEvent(MoneyAddedEvent ev)
        {
            // do smth
            Console.WriteLine("money added to account: " + ev.AccountId + " , amount: " + ev.Amount);
        }

        public void OnMoneyTakenEvent(MoneyTakenEvent ev)
        {
            // do smth
            Console.WriteLine("money taken from account: " + ev.AccountId + " , amount: " + ev.Amount);
        }


    }



}
