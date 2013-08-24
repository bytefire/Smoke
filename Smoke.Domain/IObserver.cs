using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Domain
{
    public interface IObserver
    {
        void Notify(object data);
    }
}
