namespace Kustomaur.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int ColSpan { get; set; }
        public int RowSpan { get; set; }
        
        public Position(){}

        public Position(int x, int y, int rowSpan, int colSpan)
        {
            X = x;
            Y = y;
            RowSpan = rowSpan;
            ColSpan = colSpan;
        }
    }
}