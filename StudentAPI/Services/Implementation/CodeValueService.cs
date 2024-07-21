using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CodeValueService : ICodeValueService
    {
        private readonly AppDbContext _context;

        public CodeValueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CodeValue>> GetCodeValues(int CodeId)
        {
            var CodeValues = await _context.CodeValues
                .Where(c => c.CodeId == CodeId)
                .ToListAsync();

            return CodeValues;
        }
    }
}
