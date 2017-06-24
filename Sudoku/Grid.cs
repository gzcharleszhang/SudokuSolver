using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sudoku
{
    class Grid
    {
        private int _value;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value >= 0 && value <= 9)
                {
                    _value = value;
                }
            }
        }
        private SizeF _size;

        public SizeF Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }
        private PointF _point;

        public PointF Point
        {
            get
            {
                return _point;
            }
            set
            {
                _point = value;
            }
        }
        private RectangleF _box;

        public RectangleF Box
        {
            get
            {
                return _box;
            }
            set
            {
                _box = value;
            }
        }
        private StateType _state;

        public StateType State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        

        public Grid(SizeF size, PointF point, StateType state)
        {
            _size = size;
            _point = point;
            _box = new RectangleF();
            _box.Location = _point;
            _box.Size = _size;
            _state = state;
            _value = 0;
        }
    }
}
