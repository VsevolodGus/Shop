using Shop.Domain.InterfaceRepository;

namespace Shop.Manager
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
    }
}
