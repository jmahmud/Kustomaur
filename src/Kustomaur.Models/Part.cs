namespace Kustomaur.Models
{
    public class Part
    {
        public Position Position { get; set; }
        
        public PartMetadata Metadata { get; set; }
        
        public Part WithPosition(int x, int y, int rowSpan, int colSpan)
        {
            Position = new Position(x, y, rowSpan, colSpan);
            return this;
        }
        
        public Part WithPosition(Position position)
        {
            Position = position;
            return this;
        }
    }
}