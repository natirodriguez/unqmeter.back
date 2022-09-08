using UnqMeterAPI.Interfaces;

namespace UnqMeterAPI.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
