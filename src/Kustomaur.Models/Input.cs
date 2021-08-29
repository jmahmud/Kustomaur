namespace Kustomaur.Models
{
    public class Input
    {
        public string Name { get; set; }
        public bool IsOptional { get; set; }
        
        public object Value { get; set; }

        public Input(string name)
        {
            Name = name;
            IsOptional = true;
        }
    }
}