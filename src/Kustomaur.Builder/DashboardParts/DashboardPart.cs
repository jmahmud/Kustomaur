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
        public DashboardPart WithX(int x)
        {
            _x = x;
            return this;
        }

        public DashboardPart WithY(int y)
        {
            _y = y;
            return this;
        }

        public DashboardPart WithRowSpan(int rowSpan)
        {
            _rowSpan = rowSpan;
            return this;
        }

        public DashboardPart WithColSpan(int colSpan)
        {
            _colSpan = colSpan;
            return this;
        }
    }
}