using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.TransportModel
{
    public class TaskProgress:WPFNotify
    {
        private int _current;

        private int _total;

        private bool _isIndeterminate;

        public TaskProgress() { }


        public TaskProgress(int Current, int Total) : this(Current, Total, false) { }


        public TaskProgress(int Current,int Total,bool IsIndeterminate)
        {
            this.CurrentProcess = Current;
            this.TotalProcess = Total;
            this.IsIndeterminate = IsIndeterminate;
        }

        public int CurrentProcess
        {
            get { return _current; }
            set { SetProperty<int>(ref _current, value); }
        }

        public int TotalProcess
        {
            get { return _total; }
            set { SetProperty<int>(ref _total, value); }
        }

        public bool IsIndeterminate
        {
            get { return _isIndeterminate; }
            set { SetProperty<bool>(ref _isIndeterminate, value); }
        }

        public void Reset()
        {
            this.CurrentProcess = 0;
            this.TotalProcess = 0;
            this.IsIndeterminate = false;
        }
    }
}
