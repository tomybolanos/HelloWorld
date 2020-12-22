using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface IStatusService
    {
        Task SaveStatus(DAL.DAO.Status status);

        DAL.DAO.Status GetStatus();
    }
}
