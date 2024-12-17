using CompanyApi.Models;

namespace BlazorServerApp.Services
{
    public interface IMyService
    {
        public void AddAccount(AccountDTO account);
        public Task<List<AccountDTO>> GetAccounts();
    }
}
