﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.DBContext
{
    public interface IDBContext
    {
        T GetDataBase<T>();
    }
}
