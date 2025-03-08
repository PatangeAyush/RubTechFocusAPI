using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class RegisterBAL
    {
        RegisterDAL registerDAL = new RegisterDAL();

        public RegisterDTO GetRegisteredUsers()
        {
            return registerDAL.GetRegisteredUsers();
        }

        public RegisterDTO AddUser(RegisterDTO.RegisterEntity add)
        {
            return registerDAL.AddUser(add);
        }

        public void UpdateUser(RegisterDTO.RegisterEntity update)
        {
            registerDAL.UpdateUser(update);
        }

        public void DeleteRegistredUser(int id)
        {
            registerDAL.DeleteRegistredUser(id);
        }
    }
}
