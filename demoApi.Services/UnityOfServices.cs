using demoApi.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace demoApi.Services
{
    public class UnityOfServices
    {
        public UnityOfRepository unityOfRepository { get; set; } = new UnityOfRepository();
        public LoginServices _loginServices { get; private set; }
        public UserServices _userServices { get; private set; }
        public RightsServices _rightsServices { get; private set; }
        public UnityOfServices()
        {
            _loginServices = new LoginServices(unityOfRepository);

            _userServices = new UserServices(unityOfRepository);

            _rightsServices = new RightsServices(unityOfRepository);
        }
    }
}
