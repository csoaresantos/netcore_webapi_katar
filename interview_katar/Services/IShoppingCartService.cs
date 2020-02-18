using System;
using System.Collections.Generic;

namespace interview_katar.Services
{
    public interface IShoppingCartService
    {
        ShoppingItem Add(ShoppingItem item);
        IEnumerable<ShoppingItem> GetAll();
        ShoppingItem GetById(int id);
        void Remove(int id);
    }
}
