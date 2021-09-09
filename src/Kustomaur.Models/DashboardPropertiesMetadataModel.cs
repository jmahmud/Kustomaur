namespace Kustomaur.Models
{
    public class DashboardPropertiesMetadataModel
    {
        public object Value { get; set; }

        public T ValueAs<T>() => (T)Value;
    }
}