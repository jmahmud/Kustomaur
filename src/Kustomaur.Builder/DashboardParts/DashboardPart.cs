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
        public void WithX(int x)
        {
            _x = x;
        }

        public void WithY(int y)
        {
            _y = y;
        }

        public void WithRowSpan(int rowSpan)
        {
            _rowSpan = rowSpan;
        }

        public void WithColSpan(int colSpan)
        {
            _colSpan = colSpan;
        }
    }
}