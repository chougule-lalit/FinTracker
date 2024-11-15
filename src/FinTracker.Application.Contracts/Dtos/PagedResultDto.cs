﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTracker.Application.Contracts.Dtos
{
    public class PagedResultDto<T> where T : class
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = new();
    }
}
