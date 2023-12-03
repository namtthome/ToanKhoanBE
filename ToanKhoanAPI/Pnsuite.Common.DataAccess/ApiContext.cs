using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace vn.com.pnsuite.common.dataaccess
{
    public class ApiContext : DbContext
    {
        public ApiContext() { }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

    }
}
