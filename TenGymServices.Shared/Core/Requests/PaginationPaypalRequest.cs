namespace TenGymServices.Shared.Core.Requests
{
    public class PaginationPaypalRequest
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
        public bool TotalRequired { get; set; } = true;
    }
}