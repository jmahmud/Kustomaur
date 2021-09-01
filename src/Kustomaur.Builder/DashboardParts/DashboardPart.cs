using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts
{
    public abstract class DashboardPart
    {
        protected int _x;
        protected  int _y;
        protected  int _rowSpan;
        protected  int _colSpan;
        protected Part _part;

        public DashboardPart()
        {
            _part = new Part();
        }
        public virtual Part GeneratePart()
        {
            return null;
        }
        public void SetX(int x)
        {
            _x = x;
        }

        public void SetY(int y)
        {
            _y = y;
        }

        public void SetRowSpan(int rowSpan)
        {
            _rowSpan = rowSpan;
        }

        public void SetColSpan(int colSpan)
        {
            _colSpan = colSpan;
        }
    }
}