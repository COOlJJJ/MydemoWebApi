using demoApi.Model;
using System;
using System.Collections;

namespace demoApi.Repository
{
    /// <summary>
    /// 仓储工作单元
    /// </summary>
    public class UnityOfRepository
    {

        public LoginRepository _loginRepository { get; private set; }

        public UserRepository _userRepository { get; private set; }
        public RightsRepository _rightsRepository { get; private set; }


        public UnityOfRepository()
        {
            _loginRepository = new LoginRepository();

            _userRepository = new UserRepository();

            _rightsRepository = new RightsRepository();
        }


    }
}
