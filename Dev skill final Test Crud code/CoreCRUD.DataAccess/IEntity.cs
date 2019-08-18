using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCRUD.DataAccess
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
