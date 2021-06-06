using Panda.Data.Models;
using System.Linq;

namespace Panda.Services
{
    public interface IReceiptsService
    {
        void Create(decimal weight, string packageId, string recepientId);

        IQueryable<Receipt> GetAll();

    }
}
