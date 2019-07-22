using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.DataPrep
{
    public class TypePrepBase<T> where T : class 
    {
        protected ITestDataAccessor dataAccessor;
        protected Faker random = new Faker();
        
        public TypePrepBase(ITestDataAccessor dataAccessor)
        {
            this.dataAccessor = dataAccessor;
        }

        public virtual T Create()
        {
            Faker<T> faker = new Faker<T>();
            T saved = Create(faker.Generate());
            return saved;
        }

        public virtual T Create(T existing, bool isActive = true)
        {        
            T savedModel = dataAccessor.Create(existing);      
            return savedModel;
        }

    }
}
