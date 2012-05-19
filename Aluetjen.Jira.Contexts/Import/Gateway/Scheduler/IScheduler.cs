using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aluetjen.Jira.Contexts.Import.Gateway.Scheduler
{
    public interface IScheduler
    {
        void ScheduleSync();
        void ClearSchedule();
    }
}
