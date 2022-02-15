using Shop.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Memory.Repository
{
    public class SalePointRepository : ISalePointRepository
    {

        private readonly DbContextFactory dbContextFactory;

        public SalePointRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
    }
}
