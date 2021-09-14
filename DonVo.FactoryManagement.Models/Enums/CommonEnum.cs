namespace DonVo.FactoryManagement.Models.Enums
{
    public enum TransactionPurpose
    {
        SalesReturnPayment,
        PurchaseReturnPayment,
        SalesPayment,
        PurchasePayment,
        Income,
        Expense
    }

    public enum PayablePurpose
    {
        StaffPaymentDue,
        PurchaseDue,
        PurchaseReturnDue,
        SalesReturnDue
    }

    public enum StatusItem
    {
        GOOD,
        BAD,
        DEFECTED
    }

    public enum StockInSource
    {
        Purchase,
        Production
    }

    public enum MonthFormat
    {
        MMMM
    }

    public enum TypeIncome
    {
        Sales,
        ClientPaymentRecieved,
        PurchaseReturn
    }

    public enum TypeExpense
    {
        Purchase,
        StaffPayment,
        SupplierPayment,
        SalesReturn

    }

    public enum TypeInvoice
    {
        StaffPayment,
        ClientPayment,
        SupplierPayment,
        Purchase,
        Sales,
        StaffProduction,
        SalesReturn,
        PurchaseReturn
    }
}
