using demoApi.Model;
using demoApi.Model.Rights;
using demoApi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demoApi.Services
{
    public class RightsServices
    {
        private readonly UnityOfRepository _unityOfRepository;

        public RightsServices(UnityOfRepository unityOfRepository)
        {
            _unityOfRepository = unityOfRepository;

        }

        public IEnumerable<Permission_Model> GetRightsList()
        {
            return _unityOfRepository._rightsRepository.GetRightsList();
        }

        public async Task<int> AddNewRights(Permission_Model permission_Model)
        {
            return await _unityOfRepository._rightsRepository.AddNewRights(permission_Model);
        }

        public IEnumerable<RolesList_Model> GetRolesList()
        {
            return _unityOfRepository._rightsRepository.GetRolesList();
        }

        public async Task<int> EditRoleName(int roleid, string name)
        {
            return await _unityOfRepository._rightsRepository.EditRoleName(roleid, name);
        }
        /// <summary>
        /// 获取权限树
        /// </summary>
        public async Task<IEnumerable<PermissionList_Model>> GetRightsTree()
        {
            return await _unityOfRepository._rightsRepository.GetRightsTree();
        }

        public async Task<int> GiveRights(int roleid, string[] listId)
        {
            return await _unityOfRepository._rightsRepository.GiveRights(roleid, listId);
        }

        public async Task<int> AddNewRole(string name)
        {
            return await _unityOfRepository._rightsRepository.AddNewRole(name);
        }

        public async Task<int> DeleteRole(int roleID)
        {
            return await _unityOfRepository._rightsRepository.DeleteRole(roleID);
        }

        public IEnumerable<Permission_Model> MatchPermission(string roleName)
        {
            return _unityOfRepository._rightsRepository.MatchPermission(roleName);
        }
    }
}
