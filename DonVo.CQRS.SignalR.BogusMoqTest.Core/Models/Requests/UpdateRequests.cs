namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests
{
    public record UpdateEmployeeRequest(string departmentId, string Name, string Role, string Email, string Password);
    public record UpdateDepartmentRequest(string Name);
    public record UpdateCollectionPointRequest(string Name, string Time);
    public record UpdateSupplierRequest(string Name, string ContactName, string PhoneNo, string FaxNo, string Address, string GSTReg);
    public record UpdateCustomRequestDetailsRequest(string Category, string Description, string ItemId, string Qty);
    public record UpdateInventoryItemRequest(string Id, int ItemCategoryId, string Description, string Bin, int RequestQty, int QtyInStock, int ReorderLevel, int ReorderQty, string UOM);
    public record UpdateItemCategoryRequest(string Name);
}
