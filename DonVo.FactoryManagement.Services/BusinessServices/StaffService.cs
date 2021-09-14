using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Contracts.ServiceContracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.Enums;
using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Payment;
using DonVo.FactoryManagement.Models.ViewModels.Staff;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.BusinessServices
{
    public class StaffService : IStaffService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUtilService _utilService;

        public StaffService(IRepositoryWrapper repositoryWrapper, IUtilService utilService)
        {
            this._repositoryWrapper = repositoryWrapper;
            this._utilService = utilService;
        }

        #region Db Operation
        public async Task<WrapperStaffListVM> Add(StaffVM ViewModel)
        {
            var itemToAdd = _utilService.GetMapper().Map<StaffVM, Staff>(ViewModel);
            itemToAdd = _repositoryWrapper.Staff.Create(itemToAdd);
            Task<int> t1 = _repositoryWrapper.Staff.SaveChangesAsync();

            if (ViewModel.IsManager)
            {
                UserAuthInfo infoToAdd = new()
                {
                    FactoryId = ViewModel.FactoryId,
                    Password = ViewModel.Password,
                    UserId = itemToAdd.Id,
                    UserName = ViewModel.UserName,
                };
                infoToAdd = _repositoryWrapper.UserAuthInfo.Create(infoToAdd);

                UserRole userRoleToAdd = new()
                {
                    FactoryId = ViewModel.FactoryId,
                    RoleId = ViewModel.RoleId,
                    UserId = infoToAdd.Id
                };
                userRoleToAdd = _repositoryWrapper.UserRole.Create(userRoleToAdd);
                Task<int> t2 = _repositoryWrapper.UserAuthInfo.SaveChangesAsync();
                Task<int> t3 = _repositoryWrapper.UserRole.SaveChangesAsync();
                await Task.WhenAll(t2, t3);
            }

            await Task.WhenAll(t1);
            this._utilService.Log("Successful In Adding Data");

            var dataParam = new GetDataListVM()
            {
                FactoryId = itemToAdd.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperStaffListVM data = await GetListPaged(dataParam, true);
            return data;
        }

        public async Task<WrapperStaffListVM> AddToIT_Admin(StaffVM ViewModel)
        {
            //var itemToAdd = _utilService.GetMapper().Map<StaffVM, Staff>(ViewModel);
            //itemToAdd = _repositoryWrapper.Staff.Create(itemToAdd);
            //Task<int> t1 = _repositoryWrapper.Staff.SaveChangesAsync();

            //if (ViewModel.IsManager)
            //{

            UserAuthInfo infoToAdd = new()
            {
                FactoryId = ViewModel.FactoryId,
                Password = ViewModel.Password,
                UserId = ViewModel.Id,
                UserName = ViewModel.UserName,
            };

            infoToAdd = _repositoryWrapper.UserAuthInfo.Create(infoToAdd);

            UserRole userRoleToAdd = new()
            {
                FactoryId = ViewModel.FactoryId,
                RoleId = ViewModel.RoleId,
                UserId = infoToAdd.Id
            };
            userRoleToAdd = _repositoryWrapper.UserRole.Create(userRoleToAdd);
            Task<int> t2 = _repositoryWrapper.UserAuthInfo.SaveChangesAsync();
            Task<int> t3 = _repositoryWrapper.UserRole.SaveChangesAsync();
            await Task.WhenAll(t2, t3);
            // }
            //await Task.WhenAll(t1);
            this._utilService.Log("Successful In Adding Data");

            var dataParam = new GetDataListVM()
            {
                FactoryId = ViewModel.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperStaffListVM data = await GetListPaged(dataParam, true);
            return data;
        }

        public async Task<WrapperStaffListVM> Update(string id, StaffVM ViewModel)
        {
            if (id != ViewModel.Id)
            {
                _ = new WrapperStaffListVM();
            }

            Task<IEnumerable<Staff>> itemsDB = _repositoryWrapper.Staff.FindByConditionAsync(x => x.Id == id && x.FactoryId == ViewModel.FactoryId);
            await Task.WhenAll(itemsDB);

            var itemUpdated = _utilService.GetMapper().Map<StaffVM, Staff>(ViewModel, itemsDB.Result.ToList().FirstOrDefault());
            _repositoryWrapper.Staff.Update(itemUpdated);

            Task<int> t1 = _repositoryWrapper.Staff.SaveChangesAsync();
            await Task.WhenAll(t1);
            this._utilService.Log("Successful In Updating Data");

            var dataParam = new GetDataListVM()
            {
                FactoryId = ViewModel.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperStaffListVM data = await GetListPaged(dataParam, true);
            return data;
        }

        public async Task<WrapperStaffListVM> GetListPaged(GetDataListVM dataListVM, bool withHistory)
        {
            IEnumerable<Staff> ListTask = await _repositoryWrapper.Staff.FindByConditionAsync(x => x.FactoryId == dataListVM.FactoryId);
            long noOfRecordTask = await _repositoryWrapper.Staff.NumOfRecord();

            List<Staff> List = ListTask.ToList().OrderByDescending(x => x.UpdatedDateTime).ToList();//.Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize).Take(dataListVM.PageSize).OrderByDescending(x => x.CreatedDateTime).ToList();

            List<StaffVM> outputList = new();

            outputList = _utilService.GetMapper().Map<List<Staff>, List<StaffVM>>(List);

            List<UserRole> userRoleListT = await
                _repositoryWrapper
                .UserRole
                .FindAll()
                .Where(x => x.FactoryId == dataListVM.FactoryId)
                .Include(x => x.Role)
                .Include(x => x.UserAuthInfo)
                .ToListAsync();

            for (int i = 0; i < userRoleListT.Count; i++)
            {
                StaffVM staff = outputList.Where(x => x.Id == userRoleListT.ElementAt(i).UserAuthInfo.UserId).FirstOrDefault();
                if (staff != null)
                {
                    staff.Role = userRoleListT.ElementAt(i).Role.Name;
                    staff.UserName = userRoleListT.ElementAt(i).UserAuthInfo.UserName;
                }
            }

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

            outputList = outputList.Skip((dataListVM.PageNumber - 1) * dataListVM.PageSize).Take(dataListVM.PageSize).ToList();
            var data = new WrapperStaffListVM
            {
                ListOfData = outputList,
                TotalRecords = noOfRecordTask
            };
            this._utilService.Log("Successful In Getting Data");
            if (withHistory)
            {
                data = await SetHistoryOverview(data, dataListVM.FactoryId);
            }
            return data;
        }

        public async Task<WrapperStaffListVM> Delete(StaffVM Temp)
        {
            var Task = await _repositoryWrapper.Staff.FindByConditionAsync(x => x.Id == Temp.Id && x.FactoryId == Temp.FactoryId);
            var datarow = Task.ToList().FirstOrDefault();
            if (datarow == null)
            {
                return new WrapperStaffListVM();
            }
            _repositoryWrapper.Staff.Delete(datarow);
            await _repositoryWrapper.Staff.SaveChangesAsync();
            this._utilService.Log("Successful In Deleting Data");
            var dataParam = new GetDataListVM()
            {
                FactoryId = Temp.FactoryId,
                PageNumber = 1,
                PageSize = 10,
                TotalRows = 0
            };

            WrapperStaffListVM data = await GetListPaged(dataParam, true);
            return data;
        }
        #endregion

        #region Payment
        public async Task<WrapperPaymentListVM> GetStaffPaymentList(GetPaymentDataListVM vm)
        {
            Task<List<Invoice>> paymentInvoiceListT = _repositoryWrapper
                       .Invoice
                       .FindAll()
                       .Include(x => x.InvoiceType)
                       .Where(x =>
                       x.InvoiceType.Name == TypeInvoice.StaffPayment.ToString()
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

        public async Task<WrapperPaymentListVM> PayToStaff(PaymentVM paymentVM)
        {
            Invoice invoiceToAdd = new();
            invoiceToAdd = _utilService.Mapper.Map<PaymentVM, Invoice>(paymentVM);
            _repositoryWrapper.Invoice.Create(invoiceToAdd);
            paymentVM.InvoiceId = invoiceToAdd.Id;

            Expense expenseToAdd = new();
            expenseToAdd = _utilService.Mapper.Map<PaymentVM, Expense>(paymentVM);
            _repositoryWrapper.Expense.Create(expenseToAdd);

            TblTransaction transactionToAdd = new();
            transactionToAdd = _utilService.Mapper.Map<PaymentVM, TblTransaction>(paymentVM);
            transactionToAdd.PaymentStatus = PAYMENT_STATUS.CASH_PAID.ToString();
            transactionToAdd.TransactionType = TRANSACTION_TYPE.DEBIT.ToString();
            transactionToAdd.Description = "Paid to " + paymentVM.ClientName;
            _repositoryWrapper.Transaction.Create(transactionToAdd);

            Task<int> t1 = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> t2 = _repositoryWrapper.Expense.SaveChangesAsync();
            Task<int> t3 = _repositoryWrapper.Transaction.SaveChangesAsync();

            await Task.WhenAll(t1, t2, t3);

            var item = new GetPaymentDataListVM
            {
                ClientId = paymentVM.ClientId,
                FactoryId = paymentVM.FactoryId,
                PageNumber = 1,
                PageSize = 10
            };

            return await GetStaffPaymentList(item);
        }

        public async Task<WrapperPaymentListVM> DeleteStaffPayment(PaymentVM vm)
        {
            Task<List<Invoice>> paymentInvoiceListT = _repositoryWrapper
              .Invoice
              .FindAll()
              .Include(x => x.InvoiceType)
              .Where(x =>
               x.InvoiceType.Name == TypeInvoice.StaffPayment.ToString()
              && x.ClientId == vm.ClientId
              && x.FactoryId == vm.FactoryId
              && vm.InvoiceId == x.Id)
              .ToListAsync();

            Task<List<Expense>> paymentExpenseListT = _repositoryWrapper
              .Expense
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

            await Task.WhenAll(paymentInvoiceListT, paymentExpenseListT, paymentTransactionListT);

            _repositoryWrapper.Transaction.Delete(paymentTransactionListT.Result.FirstOrDefault());
            _repositoryWrapper.Invoice.Delete(paymentInvoiceListT.Result.FirstOrDefault());
            _repositoryWrapper.Expense.Delete(paymentExpenseListT.Result.FirstOrDefault());

            Task<int> t1 = _repositoryWrapper.Transaction.SaveChangesAsync();
            Task<int> t2 = _repositoryWrapper.Invoice.SaveChangesAsync();
            Task<int> t3 = _repositoryWrapper.Expense.SaveChangesAsync();

            await Task.WhenAll(t1, t2, t3);

            var item = new GetPaymentDataListVM
            {
                ClientId = vm.ClientId,
                FactoryId = vm.FactoryId,
                PageNumber = 1,
                PageSize = 10
            };

            return await GetStaffPaymentList(item);
        }
        #endregion

        #region History
        public StaffHistory GetStaffHistoryOverview(WrapperStaffHistory list)
        {
            StaffHistory history = new();
            for (int i = 0; i < list.ListOfData.Count; i++)
            {
                StaffHistory temp = list.ListOfData.ElementAt(i);
                if (temp.Type == "InvoiceItem")
                {
                    // history.PaidAmount += temp.PaidAmount;
                    // history.PayableAmount += temp.PayableAmount;
                }
                else if (temp.Type == "ProductionItem")
                {

                }
                else if (temp.Type == "ExpenseItem")
                {
                    history.PaidAmount += temp.PaidAmount;
                }
                else if (temp.Type == "IncomeItem")
                {
                    history.RecievedAmount += temp.RecievedAmount;
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

        private async Task<WrapperStaffListVM> SetHistoryOverview(WrapperStaffListVM vm, string factoryId)
        {
            var data = new GetDataListHistory();
            //Task<WrapperStaffHistory>[] listOftask = new Task<WrapperStaffHistory>[vm.ListOfData.Count];
            WrapperStaffHistory[] listOftask = new WrapperStaffHistory[vm.ListOfData.Count];
            for (int i = 0; i < vm.ListOfData.Count; i++)
            {
                StaffVM temp = vm.ListOfData.ElementAt(i);
                data.ClientId = temp.Id;
                data.PageNumber = -1;
                data.FactoryId = factoryId;
                data.PageSize = -1;
                listOftask[i] = await GetStaffHistory(data);
                // StaffHistory te = GetStaffHistoryOverview(listOftask[i]);
                int len = listOftask[i].ListOfData.Count - 1;
                vm.ListOfData.ElementAt(i).PaidAmount = listOftask[i].ListOfData[len].PaidAmount;
                vm.ListOfData.ElementAt(i).ReceivableAmount = listOftask[i].ListOfData[len].ReceivableAmount;
                vm.ListOfData.ElementAt(i).RecievedAmount = listOftask[i].ListOfData[len].RecievedAmount;
                vm.ListOfData.ElementAt(i).PayableAmount = listOftask[i].ListOfData[len].PayableAmount;
            }

            // await Task.WhenAll(listOftask);
            //for (int i = 0; i < vm.ListOfData.Count; i++)
            //{

            //}

            return vm;
        }

        public async Task<WrapperStaffHistory> GetStaffHistory(GetDataListHistory staffVM)
        {
            // Production
            // Expense -- Payment Invoice Generated
            // Invoice

            WrapperStaffHistory wrapperStaffHistory = new();
            WrapperStaffHistory returnWrapperStaffHistory = new();
            Task<List<Production>> listProductionT =
                                    _repositoryWrapper
                                    .Production
                                    .FindAll()
                                    .Where(x => x.FactoryId == staffVM.FactoryId
                                    && x.StaffId == staffVM.ClientId)
                                    .Include(x => x.Item)
                                    .ThenInclude(x => x.ItemCategory)
                                    .ToListAsync();

            Task<List<Invoice>> listInvoiceT =
                                    _repositoryWrapper
                                    .Invoice
                                    .FindAll()
                                    .Where(x => x.FactoryId == staffVM.FactoryId
                                    && x.ClientId == staffVM.ClientId)
                                    .Include(x => x.InvoiceType)
                                    .Where(x => x.InvoiceType.Name == TypeInvoice.StaffProduction.ToString())
                                    .ToListAsync();
            Task<List<Expense>> listExpenseT =
                                    _repositoryWrapper
                                    .Expense
                                    .FindAll()
                                    .Where(x => x.FactoryId == staffVM.FactoryId
                                     && x.ClientId == staffVM.ClientId)
                                    .Include(x => x.ExpenseType)
                                    //.Where(x => x.ExpenseType.Name == TypeExpense.StaffPayment.ToString())
                                    .ToListAsync();
            Task<List<Income>> listIncomeT =
                                    _repositoryWrapper
                                    .Income
                                    .FindAll()
                                    .Where(x => x.FactoryId == staffVM.FactoryId
                                     && x.ClientId == staffVM.ClientId)
                                    .Include(x => x.IncomeType)
                                    //.Where(x => x.ExpenseType.Name == TypeExpense.StaffPayment.ToString())
                                    .ToListAsync();

            Task<List<Payable>> listPayableT =
                                   _repositoryWrapper
                                   .Payable
                                   .FindAll()
                                   .Where(x => x.FactoryId == staffVM.FactoryId
                                    && x.ClientId == staffVM.ClientId)
                                   .ToListAsync();

            Task<List<Receivable>> listReceivableT =
                                    _repositoryWrapper
                                    .Receivable
                                    .FindAll()
                                    .Where(x => x.FactoryId == staffVM.FactoryId
                                     && x.ClientId == staffVM.ClientId)
                                    .ToListAsync();

            await Task.WhenAll(listProductionT, listExpenseT, listInvoiceT, listIncomeT, listPayableT, listReceivableT);

            List<StaffHistory> production = _utilService.Mapper.Map<List<Production>, List<StaffHistory>>(listProductionT.Result.ToList());
            List<StaffHistory> expense = _utilService.Mapper.Map<List<Expense>, List<StaffHistory>>(listExpenseT.Result.ToList());
            List<StaffHistory> invoice = _utilService.Mapper.Map<List<Invoice>, List<StaffHistory>>(listInvoiceT.Result.ToList());
            List<StaffHistory> income = _utilService.Mapper.Map<List<Income>, List<StaffHistory>>(listIncomeT.Result.ToList());
            List<StaffHistory> payable = _utilService.Mapper.Map<List<Payable>, List<StaffHistory>>(listPayableT.Result.ToList());
            List<StaffHistory> Receivable = _utilService.Mapper.Map<List<Receivable>, List<StaffHistory>>(listReceivableT.Result.ToList());

            wrapperStaffHistory.ListOfData.AddRange(invoice);
            wrapperStaffHistory.ListOfData.AddRange(expense);
            wrapperStaffHistory.ListOfData.AddRange(income);
            wrapperStaffHistory.ListOfData.AddRange(payable);
            wrapperStaffHistory.ListOfData.AddRange(Receivable);

            wrapperStaffHistory.ListOfData = wrapperStaffHistory.ListOfData.OrderByDescending(x => x.OccurranceDate).ToList();
            for (int i = 0; i < wrapperStaffHistory.ListOfData.Count; i++)
            {
                List<StaffHistory> tempList = new();
                if (wrapperStaffHistory.ListOfData.ElementAt(i).Type == "InvoiceItem")
                {
                    tempList = production.Where(x => x.InvoiceId == wrapperStaffHistory.ListOfData.ElementAt(i).InvoiceId).ToList();
                    returnWrapperStaffHistory.ListOfData.AddRange(tempList);
                    returnWrapperStaffHistory.ListOfData.Add(wrapperStaffHistory.ListOfData.ElementAt(i));
                }
                else
                {
                    returnWrapperStaffHistory.ListOfData.Add(wrapperStaffHistory.ListOfData.ElementAt(i));
                }
            }

            var staffHist = GetStaffHistoryOverview(returnWrapperStaffHistory);
            //  returnWrapperStaffHistory.ListOfData = returnWrapperStaffHistory.ListOfData.OrderByDescending(x => x.OccurranceDate).ToList();
            if (staffVM.PageSize != -1)
            {
                returnWrapperStaffHistory.ListOfData = returnWrapperStaffHistory.ListOfData
                  .Skip((staffVM.PageNumber - 1) * staffVM.PageSize)
                  .Take(staffVM.PageSize)
                  .ToList();
            }

            returnWrapperStaffHistory.ListOfData.Add(staffHist);
            return returnWrapperStaffHistory;
        }
        #endregion
    }
}
