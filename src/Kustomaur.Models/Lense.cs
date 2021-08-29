namespace Kustomaur.Models
{
    public class Lense
    {
        public int Order { get; set; }

        public Parts Parts { get; set; }

        public Lense WithParts(Parts parts)
        {
            Parts = parts;
            return this;
        }
    }
}