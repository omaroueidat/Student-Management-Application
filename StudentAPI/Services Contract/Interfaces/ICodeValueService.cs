using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContract
{
    public interface ICodeValueService
    {
        public Task<List<CodeValue>> GetCodeValues(int CodeId);
    }
}
