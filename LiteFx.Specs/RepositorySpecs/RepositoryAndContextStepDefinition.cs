using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using LiteFx.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.RepositorySpecs
{
    [Binding]
    public class RepositoryAndContextStepDefinition
    {
        public static IOrdinaryContext context;
        IOrdinaryEntityRepository repository;

        Entity entityInstance;
        IEnumerable<Entity> entityCollection;

        [Given(@"a Context")]
        public void GivenAContext()
        {
            context = new OrdinaryContext();
        }

        [Given(@"a Repository")]
        public void GivenARepository()
        {
            repository = new OrdinaryEntityRepository();
        }

        [When(@"I call the GetById method using the valid id (.*)")]
        public void WhenICallTheGetByIdMethodUsingTheValidId(int id)
        {
            entityInstance = repository.GetById(id);
        }

        [When(@"I call the GetAll method")]
        public void WhenICallTheGetAllMethod()
        {
            entityCollection = repository.GetAll();
        }

        [Then(@"a entity instance with the id (.*) should be returned")]
        public void ThenAEntityInstanceWithTheId_ShouldBeReturned(int id)
        {
            Assert.AreEqual(entityInstance.Id, id);
        }

        [Then(@"a entity collection should be returned")]
        public void ThenAEntityCollectionShouldBeReturned()
        {
            Assert.AreEqual(entityCollection.Count(), 1);
        }
    }
}
