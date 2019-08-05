using System;
using System.Collections.Generic;
using System.Text;
using TestingHomework.Tests.DTOs;
using Bogus;
using TestingHomework.Tests.DataPrep;

namespace TestingHomework.Tests.Data
{
    public class TypePrepBase<T, PersistedType> where T : class where PersistedType : class, IDatabaseObjectBase
    {
           ITestDataAccessor dataAccessor;
           Bogus.Faker random = new Bogus.Faker();

        protected MapperBase<T, PersistedType> mapper = new MapperBase<T, PersistedType>();

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
            PersistedType model = mapper.ContractToModel(existing);
            // handle active state here so I can create inactive items, but leave active flags off of data contracts
            model.IsActive = isActive;

            PersistedType savedModel = dataAccessor.Create(model);
            T savedContract = mapper.ModelToContract(savedModel);

            return savedContract;
        }

    }
}
