using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Raven.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void Setup() { SessionFactoryManager.Current.GetCurrentSession(); }

        [TestMethod][Ignore]
        public void TestMethod1()
        {
            var random = new System.Random();
            for (int i = 0; i < 150; i++)
            {
                SessionFactoryManager.Current.GetCurrentSession().Store(new Usuario() { Nome = string.Format("Douglas Aguiar {0}", i), Email = "doaguiar@gmail.com", Idade = random.Next(12, 29) });
            }

            SessionFactoryManager.Current.CommitTransaction();
        }

        [TestMethod][Ignore]
        public void select_usuarios_UsuariosMenoresDeIdade()
        {
            var list = SessionFactoryManager.Current.GetCurrentSession().Query<Usuario>("UsuariosMenoresDeIdade").ToList();

            Assert.AreEqual(45, list.Count);
        }

        [TestMethod][Ignore]
        public void select_usuarios_UsuariosMaioresDeIdade()
        {
            var list = SessionFactoryManager.Current.GetCurrentSession().Query<Usuario>("UsuariosMaioresDeIdade").ToList();

            Assert.AreEqual(105, list.Count);
        }

        [TestMethod][Ignore]
        public void select_usuarios()
        {
            var list = SessionFactoryManager.Current.GetCurrentSession().Query<Usuario>().Take(150).ToList();

            Assert.AreEqual(150, list.Count);
        }

        public class Usuario
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public int Idade { get; set; }
            public string Email { get; set; }
        }
    }
}
