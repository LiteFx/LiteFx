using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace LiteFx.NHibernate.Specs
{
    [TestClass]
    public class PublicMethodsTest
    {
        [TestMethod]
        public void All_EntityBase_Public_Methods_Should_Be_Virtual()
        {
            Assert.IsFalse(HasNonVirtualPublicMethods(typeof(EntityBaseWithValidation<int>)));
        }

        [TestMethod]
        public void All_ValueObjectBase_Public_Methods_Should_Be_Virtual()
        {
            Assert.IsFalse(HasNonVirtualPublicMethods(typeof(ValueObjectBase)));
        }

        public bool HasNonVirtualPublicMethods(Type type) 
        {
            var methods = from m in type.GetMethods()
                          where !m.IsVirtual && !m.IsStatic && m.IsPublic && m.DeclaringType != typeof(object)
                          select m;

            return methods.Count() > 0;
        }
    }
}
