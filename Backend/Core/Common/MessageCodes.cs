using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public enum MessageCodes
    {
        Success = 0,
        NotFound = 1,
        ErrorValidation = 2,
        Authentication = 3,
        Authorization = 4,
        ErrorDataBase = 5,
        NoData = 6,
        Conflict = 7,
        Unauthorized = 8,
    }
}
