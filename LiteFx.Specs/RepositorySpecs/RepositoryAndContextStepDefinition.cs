using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using LiteFx.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LiteFx.Specs.RepositorySpecs
{
    [Binding]
    public class RepositoryAndContextStepDefinition
    {
        public static IOrdinaryContext context;
        public Moq.Mock<IOrdinaryContext> mockContext;

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

        [Given(@"a mocked Context")]
        public void GivenAMockedContext()
        {
            mockContext = new Moq.Mock<IOrdinaryContext>();
            context = mockContext.Object;
        }

        [Given(@"an Entity")]
        public void GivenAnEntity()
        {
            entityInstance = new Entity();
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

        [When(@"I call the Save method on the Repository")]
        public void WhenICallTheSaveMethodOnTheRepository()
        {
            repository.Save(entityInstance);
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

        [Then(@"the Context Save method shold be called")]
        public void ThenTheContextSaveMethodSholdBeCalled()
        {
            mockContext.Verify(c => c.Save(It.IsAny<Entity>()), Times.Once());
        }
    }
}
