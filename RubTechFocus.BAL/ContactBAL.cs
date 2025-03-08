using RubTechFocus.DAL;
using RubTechFocus.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubTechFocus.BAL
{
    public class ContactBAL
    {
        ContactDAL objcontactDAL = new ContactDAL();

        public List<ContactDTO> GetContacts()
        {
            return objcontactDAL.GetContacts();
        }

        public void AddContact(ContactDTO add)
        {
            objcontactDAL.AddContact(add);
        }
        public ContactDTO UpdateContact(ContactDTO update)
        {
            return objcontactDAL.UpdateContact(update);
        }

        public void DeleteContact(int id)
        {
            objcontactDAL.DeleteContact(id);
        }
    }
}
