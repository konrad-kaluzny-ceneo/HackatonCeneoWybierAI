using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model.Interfaces
{
    public interface IResultsRepository
    {
        Results GetResults(int sessionId);
        int SaveResults(Results results);
    }
}
