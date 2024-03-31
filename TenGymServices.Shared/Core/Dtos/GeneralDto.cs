namespace TenGymServices.Shared.Core.Dtos
{
    public class GeneralDto<T> where T : class, new ()
    {
        public T? Result { get; set; }
        public ErrorMessage? Error { get; set; }
    }

    public class ErrorMessage 
    {
        public string? Name { get; set; }
        public string? Message { get; set; }
        public List<DetailsMessage>? Details { get; set; } = null;
    }

    public class DetailsMessage
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public string Location { get; set; }
        public string Issue { get; set; }
        public string Description { get; set; }
    }
}