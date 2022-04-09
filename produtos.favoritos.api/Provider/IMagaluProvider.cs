﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider
{
    public interface IMagaluProvider
    {
        Task<ProductEntity> GetAsync(Guid id);
    }
}
