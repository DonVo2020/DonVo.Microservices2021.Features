using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.Enums;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.CustomerView;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;
        public CustomerService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }


        #region Db Opeartion
        public async Task<WrapperListCustomerVM> Add(CustomerVM addCustomerViewModel)
        {
            var CustomerDT = _utilService.GetMapper().Map<CustomerVM, Customer>(addCustomerViewModel);
            //   var AddressDT = _mapper.Map<AddCustomerViewModel, Address>(addCustomerViewModel);
            //  var PhoneDT = _mapper.Map<AddCustomerViewModel, Phone>(addCustomerViewModel);

            //Task<string> cusUnique = _repositoryWrapper.Customer.GetUniqueId();
            //Task<string> addressUnique = _repositoryWrapper.Address.GetUniqueId();
            //Task<string> phoneUnique = _repositoryWrapper.Phone.GetUniqueId();

            //await Task.WhenAll(cusUnique, addressUnique, phoneUnique);

            //CustomerDT.UniqueId = cusUnique.Result;
            //AddressDT.UniqueId = addressUnique.Result;
            //PhoneDT.UniqueId = phoneUnique.Result;


            CustomerDT = _repositoryWrapper.Customer.Create(CustomerDT);
            //AddressDT.RelatedId = CustomerDT.Id;
            //AddressDT = _repositoryWrapper.Address.Create(AddressDT);
            //PhoneDT.RelatedId = CustomerDT.Id;
            //PhoneDT = _repositoryWrapper.Phone.Create(PhoneDT);
            Task<int> t1 = _repositoryWrapper.Customer.SaveChangesAsync();
            //Task<int> t2 = _repositoryWrapper.Address.SaveChangesAsync();
            //Task<int> t3 = _repositoryWrapper.Phone.SaveChangesAsync();
            //// await _repositoryWrapper.SaveAsync();
            await Task.WhenAll(t1);
            this._utilService.Log("Successful In saving. " + CustomerDT.Id);

            var dataParam = new GetDataListVM()
            {
                FactoryId = addCustomerViewModel.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperListCustomerVM data = await GetListPaged(dataParam, true);
            return data;
        }
        public async Task<WrapperListCustomerVM> Update(string id, CustomerVM updateCustomerViewModel)
        {
            if (id != updateCustomerViewModel.CustomerId)
            {
                _ = new WrapperListCustomerVM();
            }

            Task<IEnumerable<Customer>> CustomersDB = _repositoryWrapper.Customer.FindByConditionAsync(x => x.Id == id && x.FactoryId == updateCustomerViewModel.FactoryId);
            //Task<IEnumerable<Address>> AddressesDB = _repositoryWrapper.Address.FindByConditionAsync(x => x.RelatedId == id && x.FactoryId == updateCustomerViewModel.FactoryId);
            //Task<IEnumerable<Phone>> PhonesDB = _repositoryWrapper.Phone.FindByConditionAsync(x => x.RelatedId == id && x.FactoryId == updateCustomerViewModel.FactoryId);

            await Task.WhenAll(CustomersDB);

            var CustomerUpdated = _utilService.GetMapper().Map<CustomerVM, Customer>(updateCustomerViewModel, CustomersDB.Result.ToList().FirstOrDefault());
            //var AddressUpdated = _mapper.Map<UpdateCustomerViewModel, Address>(updateCustomerViewModel, AddressesDB.Result.ToList().FirstOrDefault());
            //var PhoneUpdated = _mapper.Map<UpdateCustomerViewModel, Phone>(updateCustomerViewModel, PhonesDB.Result.ToList().FirstOrDefault());
            _repositoryWrapper.Customer.Update(CustomerUpdated);
            //_repositoryWrapper.Address.Update(AddressUpdated);
            //_repositoryWrapper.Phone.Update(PhoneUpdated);

            Task<int> t1 = _repositoryWrapper.Customer.SaveChangesAsync();
            //Task<int> t2 = _repositoryWrapper.Address.SaveChangesAsync();
            //Task<int> t3 = _repositoryWrapper.Phone.SaveChangesAsync();
            await Task.WhenAll(t1);
            this._utilService.Log("Successful In saving");

            var dataParam = new GetDataListVM()
            {
                FactoryId = updateCustomerViewModel.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperListCustomerVM data = await GetListPaged(dataParam, true);
            return data;
        }
        public async Task<WrapperListCustomerVM> GetListPaged(GetDataListVM dataListVM, bool withHistory)
        {
            IEnumerable<Customer> custListTask = await _repositoryWrapper.Customer.FindByConditionAsync(x => x.FactoryId == dataListVM.FactoryId);
            //Task<IEnumerable<Address>> addressListTask = _repositoryWrapper.Address.FindByConditionAsync(x => x.FactoryId == dataListVM.FactoryId);
            //Task<IEnumerable<Phone>> phoneListTask = _repositoryWrapper.Phone.FindByConditionAsync(x => x.FactoryId == dataListVM.FactoryId);
            long noOfRecordTask = await _repositoryWrapper.Customer.NumOfRecord();
            // await Task.WhenAll(custListTask);
            // await Task.WhenAll(noOfRecordTask);

            List<Customer> custList = custListTask.ToList().OrderByDescending(x => x.UpdatedDateTime).ToList();//.Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize).Take(dataListVM.PageSize).OrderByDescending(x => x.CreatedDateTime).ToList();
            //List<Address> addressList = addressListTask.Result.ToList();
            //List<Phone> phoneList = phoneListTask.Result.ToList();

            List<CustomerVM> outputList = new();

            outputList = _utilService.GetMapper().Map<List<Customer>, List<CustomerVM>>(custList);
            if (!string.IsNullOrEmpty(dataListVM.GlobalFilter) && !string.IsNullOrWhiteSpace(dataListVM.GlobalFilter))
            {
                outputList = outputList.Where(output =>
                output.AlternateCellNo != null ? output.AlternateCellNo.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase) : false
                            || output.CellNo != null ? output.CellNo.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase) : false
                            || output.Email != null ? output.Email.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase) : false
                            || output.Name != null ? output.Name.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase) : false
                             || output.PermanentAddress != null ? output.PermanentAddress.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase) : (false
                              || output.PresentAddress != null) && output.PresentAddress.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            //for (int i = 0; i < custList.Count(); i++)
            //{
            //    var tempCustomer = custList.ElementAt(i);
            //    //var tempAddress = addressList.Where(x => x.RelatedId == tempCustomer.Id).FirstOrDefault();
            //    //var tempPhone = phoneList.Where(x => x.RelatedId == tempCustomer.Id).FirstOrDefault();
            //    var output = new ListCustomerVM();
            //    output = _mapper.Map<Customer, ListCustomerVM>(tempCustomer, output);
            //    //output = _mapper.Map<Address, ListCustomerVM>(tempAddress, output);
            //    //output = _mapper.Map<Phone, ListCustomerVM>(tempPhone, output);
            //    if (dataListVM.GlobalFilter != null)
            //    {
            //        if (output.AlternateCellNo.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase)
            //            || output.CellNo.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase)
            //            || output.Email.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase)
            //            || output.Name.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase)
            //             || output.PermanentAddress.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase)
            //              || output.PresentAddress.Contains(dataListVM.GlobalFilter, StringComparison.OrdinalIgnoreCase))
            //        {
            //            outputList.Add(output);
            //        }
            //    }
            //    else
            //    {
            //        outputList.Add(output);
            //    }

            //}

            outputList = outputList.Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize).Take(dataListVM.PageSize).ToList();

            WrapperListCustomerVM data = new()
            {
                ListOfData = outputList,
                TotalRecords = noOfRecordTask
            };
            if (withHistory)
            {
                data = await SetHistoryOverview(data, dataListVM.FactoryId);
            }

            return data;
        }

        public async Task<WrapperListCustomerVM> Delete(CustomerVM customerTemp)
        {
            var customerTask = await _repositoryWrapper.Customer.FindByConditionAsync(x => x.Id == customerTemp.CustomerId && x.FactoryId == customerTemp.FactoryId);
            var customer = customerTask.ToList().FirstOrDefault();
            if (customer == null)
            {
                return new WrapperListCustomerVM();
            }
            _repositoryWrapper.Customer.Delete(customer);
            await _repositoryWrapper.Customer.SaveChangesAsync();
            var dataParam = new GetDataListVM()
            {
                FactoryId = customerTemp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };
            WrapperListCustomerVM data = await GetListPaged(dataParam, true);
            return data;
        }
        #endregion

        #region Payment
        public async Task<WrapperPaymentListVM> GetCustomerPaymentList(GetPaymentDataListVM vm)
        {
            Task<List<Invoice>> paymentInvoiceListT = _repositoryWrapper
                       .Invoice
                       .FindAll()
                       .Include(x => x.InvoiceType)
                       .Include(x => x.Customer)
                       .Where(x =>
                       (x.InvoiceType.Name == TypeInvoice.ClientPayment.ToString()
                       || x.InvoiceType.Name == TypeInvoice.Sales.ToString()
                       || x.InvoiceType.Name == TypeInvoice.SalesReturn.ToString())
                        && x.ClientId == vm.ClientId
                        && x.FactoryId == vm.FactoryId)
                       .OrderByDescending(x => x.DateOfOcurrance)
                       .Skip((vm.PageNumber - 1) * vm.PageSize)
                       .Take(vm.PageSize)
                       .ToListAsync();

            await paymentInvoiceListT;

            WrapperPaymentListVM wrapperPaymentListVM = new()
            {
                ListOfData = _utilService.Mapper.Map<List<Invoice>, List<PaymentVM>>(paymentInvoiceListT.Result.ToList())
            };

            return wrapperPaymentListVM;
        }

        public async Task<WrapperPaymentListVM> RecieveFromCustomer(PaymentVM paymentVM)
        {
            Invoice invoiceToAdd = new();
            invoiceToAdd = _utilService.Mapper.Map<PaymentVM, Invoice>(paymentVM);
            _repositoryWrapper.Invoice.Create(invoiceToAdd);
            paymentVM.InvoiceId = invoiceToAdd.Id;

            Income incomeToAdd = new();
            incomeToAdd = _utilService.Mapper.Map<PaymentVM, Income>(paymentVM);
            _repositoryWrapper.Income.Create(incomeToAdd);

            TblTransaction transactionToAdd = new();
            transactionToAdd = _utilService.Mapper.Map<PaymentVM, TblTransaction>(paymentVM);
            transactionToAdd.PaymentStatus = PAYMENT_STATUS.CASH_RECIEVED.ToString();
            transactionToAdd.Description = " Recieved from " + paymentVM.ClientName;
            transactionToAdd.TransactionType = TRANSACTION_TYPE.CREDIT.ToString();
            _repositoryWrapper.Transaction.Create(transactionToAdd);

            Task<int> t1 = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> t2 = _repositoryWrapper.Income.SaveChangesAsync();
            Task<int> t3 = _repositoryWrapper.Transaction.SaveChangesAsync();

            await Task.WhenAll(t1, t2, t3);

            var item = new GetPaymentDataListVM
            {
                ClientId = paymentVM.ClientId,
                FactoryId = paymentVM.FactoryId,
                PageNumber = 1,
                PageSize = 10
            };

            return await GetCustomerPaymentList(item);

        }

        public async Task<WrapperPaymentListVM> DeleteCustomerPayment(PaymentVM vm)
        {
            Task<List<Invoice>> paymentInvoiceListT = _repositoryWrapper
              .Invoice
              .FindAll()
              .Include(x => x.InvoiceType)
              .Where(x =>
               x.InvoiceType.Name == TypeInvoice.ClientPayment.ToString()
              && x.ClientId == vm.ClientId
              && x.FactoryId == vm.FactoryId
              && vm.InvoiceId == x.Id)
              .ToListAsync();

            Task<List<Income>> paymentIncomeListT = _repositoryWrapper
              .Income
              .FindAll()
              .Where(x =>
               x.ClientId == vm.ClientId
              && x.FactoryId == vm.FactoryId
              && x.InvoiceId == vm.InvoiceId)
              .ToListAsync();

            Task<List<TblTransaction>> paymentTransactionListT = _repositoryWrapper
                  .Transaction
                  .FindAll()
                  .Where(x =>
                   x.ClientId == vm.ClientId
                  && x.FactoryId == vm.FactoryId
                  && x.InvoiceId == vm.InvoiceId)
                  .ToListAsync();

            await Task.WhenAll(paymentInvoiceListT, paymentIncomeListT, paymentTransactionListT);

            _repositoryWrapper.Transaction.Delete(paymentTransactionListT.Result.FirstOrDefault());
            _repositoryWrapper.Invoice.Delete(paymentInvoiceListT.Result.FirstOrDefault());
            _repositoryWrapper.Income.Delete(paymentIncomeListT.Result.FirstOrDefault());

            Task<int> t1 = _repositoryWrapper.Transaction.SaveChangesAsync();
            Task<int> t2 = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> t3 = _repositoryWrapper.Income.SaveChangesAsync();

            await Task.WhenAll(t1, t2, t3);

            var item = new GetPaymentDataListVM
            {
                ClientId = vm.ClientId,
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10
            };

            return await GetCustomerPaymentList(item);
        }
        #endregion

        #region History
        public CustomerHistory GetCustomerHistoryOverview(WrapperCustomerHistory list)
        {
            CustomerHistory history = new();
            for (int i = 0; i < list.ListOfData.Count; i++)
            {
                CustomerHistory temp = list.ListOfData.ElementAt(i);
                if (temp.Type == "InvoiceItem")
                {
                    //  history.ReceivableAmount += temp.ReceivableAmount;
                    //  history.RecievedAmount += temp.RecievedAmount;
                }
                else if (temp.Type == "SaleItem")
                {

                }
                else if (temp.Type == "IncomeItem")
                {
                    history.RecievedAmount += temp.RecievedAmount;
                }
                else if (temp.Type == "ExpenseItem")
                {
                    history.PaidAmount += temp.PaidAmount;
                }
                else if (temp.Type == "PayableItem")
                {
                    history.PayableAmount += temp.PayableAmount;
                }
                else if (temp.Type == "ReceivableItem")
                {
                    history.ReceivableAmount += temp.ReceivableAmount;
                }
            }

            return history;
        }
        private async Task<WrapperListCustomerVM> SetHistoryOverview(WrapperListCustomerVM vm, string FactoryId)
        {
            var data = new GetDataListHistory();
            //Task<WrapperCustomerHistory>[] listOftask = new Task<WrapperCustomerHistory>[vm.ListOfData.Count];
            WrapperCustomerHistory[] listOftask = new WrapperCustomerHistory[vm.ListOfData.Count];
            for (int i = 0; i < vm.ListOfData.Count; i++)
            {
                CustomerVM temp = vm.ListOfData.ElementAt(i);
                data.ClientId = temp.CustomerId;
                data.PageNumber = -1;
                data.PageSize = -1;
                data.FactoryId = FactoryId;
                listOftask[i] = await GetCustomerHistory(data);

                int len = listOftask[i].ListOfData.Count - 1;
                vm.ListOfData.ElementAt(i).PaidAmount = listOftask[i].ListOfData[len].PaidAmount;
                vm.ListOfData.ElementAt(i).ReceivableAmount = listOftask[i].ListOfData[len].ReceivableAmount;
                vm.ListOfData.ElementAt(i).RecievedAmount = listOftask[i].ListOfData[len].RecievedAmount;
                vm.ListOfData.ElementAt(i).PayableAmount = listOftask[i].ListOfData[len].PayableAmount;
            }

            //            await Task.WhenAll(listOftask);

            //for (int i = 0; i < vm.ListOfData.Count; i++)
            //{
            //    //CustomerHistory te = GetCustomerHistoryOverview(listOftask[i].Result);
            //    CustomerHistory te = GetCustomerHistoryOverview(listOftask[i]);
            //    vm.ListOfData.ElementAt(i).PaidAmount = te.PaidAmount;
            //    vm.ListOfData.ElementAt(i).ReceivableAmount = te.ReceivableAmount;
            //    vm.ListOfData.ElementAt(i).RecievedAmount = te.RecievedAmount;
            //    vm.ListOfData.ElementAt(i).PayableAmount = te.PayableAmount;
            //}

            return vm;
        }
        public async Task<WrapperCustomerHistory> GetCustomerHistory(GetDataListHistory customerVM)
        {
            WrapperCustomerHistory custHist = new();
            WrapperCustomerHistory returnCustHist = new();
            // Sales -- Sales Invoice Generated
            // Invoice
            // Income -- Payment Invoice Generated

            Task<List<Sales>> listSalesT =
                _repositoryWrapper
                .Sales
                .FindAll()
                .Where(x => x.FactoryId == customerVM.FactoryId
                && x.ClientId == customerVM.ClientId)
                .Include(x => x.Item)
                .ThenInclude(x => x.ItemCategory)
                .ToListAsync();

            Task<List<Invoice>> listInvoiceT =
                _repositoryWrapper
                .Invoice
                .FindAll()
                .Where(x => x.FactoryId == customerVM.FactoryId
                && x.ClientId == customerVM.ClientId)
                .Include(x => x.InvoiceType)
                .Where(x => x.InvoiceType.Name == TypeInvoice.Sales.ToString())
                .ToListAsync();

            Task<List<Income>> listIncomeT =
                _repositoryWrapper
                .Income
                .FindAll()
                .Where(x => x.FactoryId == customerVM.FactoryId
                 && x.ClientId == customerVM.ClientId)
                .Include(x => x.IncomeType)
                //.Where(x => x.IncomeType.Name == TypeIncome.ClientPaymentRecieved.ToString())
                .ToListAsync();

            Task<List<Expense>> listExpenseT =
                _repositoryWrapper
                .Expense
                .FindAll()
                .Where(x => x.FactoryId == customerVM.FactoryId
                 && x.ClientId == customerVM.ClientId)
                .Include(x => x.ExpenseType)
                //.Where(x => x.IncomeType.Name == TypeIncome.ClientPaymentRecieved.ToString())
                .ToListAsync();

            Task<List<Payable>> listPayableT =
                                  _repositoryWrapper
                                  .Payable
                                  .FindAll()
                                  .Where(x => x.FactoryId == customerVM.FactoryId
                                   && x.ClientId == customerVM.ClientId)
                                  .ToListAsync();

            Task<List<Receivable>> listReceivableT =
                        _repositoryWrapper
                        .Receivable
                        .FindAll()
                        .Where(x => x.FactoryId == customerVM.FactoryId
                         && x.ClientId == customerVM.ClientId)
                        .ToListAsync();

            await Task.WhenAll(listSalesT, listInvoiceT, listIncomeT, listExpenseT, listPayableT, listReceivableT);

            List<CustomerHistory> custHistInvoice = _utilService.Mapper.Map<List<Invoice>, List<CustomerHistory>>(listInvoiceT.Result.ToList());
            List<CustomerHistory> custHistIncome = _utilService.Mapper.Map<List<Income>, List<CustomerHistory>>(listIncomeT.Result.ToList());
            List<CustomerHistory> custHistSales = _utilService.Mapper.Map<List<Sales>, List<CustomerHistory>>(listSalesT.Result.ToList());
            List<CustomerHistory> custHistExpense = _utilService.Mapper.Map<List<Expense>, List<CustomerHistory>>(listExpenseT.Result.ToList());
            List<CustomerHistory> custHistPayable = _utilService.Mapper.Map<List<Payable>, List<CustomerHistory>>(listPayableT.Result.ToList());
            List<CustomerHistory> custHistReceivable = _utilService.Mapper.Map<List<Receivable>, List<CustomerHistory>>(listReceivableT.Result.ToList());

            custHist.ListOfData.AddRange(custHistInvoice);
            custHist.ListOfData.AddRange(custHistIncome);
            custHist.ListOfData.AddRange(custHistExpense);
            custHist.ListOfData.AddRange(custHistPayable);
            custHist.ListOfData.AddRange(custHistReceivable);
            custHist.ListOfData = custHist.ListOfData.OrderBy(x => x.OccurranceDate).ToList();

            for (int i = 0; i < custHist.ListOfData.Count; i++)
            {
                IEnumerable<CustomerHistory> tempList = new List<CustomerHistory>();
                if (custHist.ListOfData.ElementAt(i).Type == "InvoiceItem")
                {
                    tempList = custHistSales.Where(x => x.InvoiceId == custHist.ListOfData.ElementAt(i).InvoiceId);
                    returnCustHist.ListOfData.AddRange(tempList);
                    returnCustHist.ListOfData.Add(custHist.ListOfData.ElementAt(i));
                }
                else
                {
                    returnCustHist.ListOfData.Add(custHist.ListOfData.ElementAt(i));
                }
            }

            var staffHist = GetCustomerHistoryOverview(returnCustHist);

            if (customerVM.PageSize != -1)
            {
                returnCustHist.ListOfData = returnCustHist.ListOfData
                                        .Skip((customerVM.PageNumber - 1) * customerVM.PageSize)
                                        .Take(customerVM.PageSize)
                                        .ToList();
            }

            returnCustHist.ListOfData.Add(staffHist);
            return returnCustHist;
        }
        #endregion
    }
}
