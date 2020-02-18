using System;
using System.Collections.Generic;
using System.Linq;
using interview_katar;
using interview_katar.Services;

namespace web_api_test.Services
{
    public class ShoppingCartServiceFake : IShoppingCartService
    {
        private readonly List<ShoppingItem> _shoppingCart;
        public ShoppingCartServiceFake()
        {
            _shoppingCart = new List<ShoppingItem>()
            {
                new ShoppingItem
                {
                    Id =1,
                    Name="Suporte"
                },
                new ShoppingItem
                {
                    Id=2,
                    Name="Monitor"
                },
                new ShoppingItem
                {
                    Id=3,
                    Name="Teclado"
                }
            };
        }

        public IEnumerable<ShoppingItem> GetAll()
        {
            return _shoppingCart;
        }

        public ShoppingItem Add(ShoppingItem newItem)
        {
            newItem.Id = 4;
            _shoppingCart.Add(newItem);
            return newItem;
        }

        public ShoppingItem GetById(int id)
        {
            return _shoppingCart.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public void Remove(int id)
        {
            var existing = _shoppingCart.First(a => a.Id.Equals(id));
            _shoppingCart.Remove(existing);
        }
    }
}
