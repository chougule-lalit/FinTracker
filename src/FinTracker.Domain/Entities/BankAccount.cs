using FinTracker.Domain.Data;
using FinTracker.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FinTracker.Domain.Entities
{
    public class BankAccount : AuditedEntity
    {
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }  // Savings/Current
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        //public User User { get; set; }
        //public List<Transaction> Transactions { get; set; }
    }
}
