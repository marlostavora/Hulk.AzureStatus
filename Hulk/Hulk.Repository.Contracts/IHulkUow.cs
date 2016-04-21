using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Repository.Contracts
{
    /// <summary>
    /// Interface for the Hulk "Unit of Work"
    /// </summary>
    public interface IHulkUow
    {
        // Save pending changes to the data store.
        void Commit();

        // Repositories
        IServiceRepository Services { get; }        
        IRegionRepository Regions { get; }
        IServiceStatusRepository ServicesStatus { get; }

        //IRepository<Room> Rooms { get; }
    }
}
