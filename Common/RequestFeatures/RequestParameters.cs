﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 4213;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 1337;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
        public string? OrderBy { get; set; }
        public string? Fields { get; set; }
    }
}
