using BloodDonationApp.Domain.ExceptionModel.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ExceptionModel.Exceptions
{
    public sealed class VolunteerNotFoundException : NotFoundException
    {
        public VolunteerNotFoundException(string partialName) : base($"Volonter sa {partialName} u imenu/prezimenu ne postoji u nasoj bazi volontera")
        {
        }
    }
}
