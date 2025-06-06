using DotNetSpaAuth.Data;
using DotNetSpaAuth.Models;

namespace DotNetSpaAuth.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }


        public Task<User> CreateUser(User user)
        {

            this._appDbContext.Add(user);

            this._appDbContext.SaveChanges();

            return Task.FromResult(user);
        }


    }
}
