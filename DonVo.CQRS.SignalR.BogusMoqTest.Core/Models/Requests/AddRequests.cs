namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests
{
    public record AddEmployeeRequest(string Name, string Role, string Email, string Password, int DepartmentId, bool? IsActingDepartmentHead);
    public record AddDepartmentRequest(int? Id, string Name, int CollectionPointId, int? DepartmentHeadId, int? DepartmentRepId);
    public record AddCollectionPointRequest(int? Id, string Name, string Time, int? EmployeeId);
    public record AddSupplierRequest(string Name, string ContactName, string PhoneNo, string FaxNo, string Address, string GSTReg);
    public record AddCustomRequestDetailsRequest(string Category, string Description, string ItemId, string Qty);
    public record AddInventoryItemRequest(string Id, int ItemCategoryId, string Description, string Bin, int RequestQty, int QtyInStock, int ReorderLevel, int ReorderQty, string UOM);
    public record AddItemCategoryRequest(string Name);
}
