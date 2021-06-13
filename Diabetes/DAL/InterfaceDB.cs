using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diabetes.Models;

namespace Diabetes.DAL
{
    public interface InterfaceDB
    {
        public List<Food> List();
        public Food GetByName(string name);
        public Food GetById(int id);
        public void Delete(int id);
        public void Edit(Food food);
        public void Add(Food food);
        public List<Category> Categories();
    }
}
