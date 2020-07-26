using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Virtualcompanion.Core.Contexts;

namespace VirtualCompanion.Core.Test.Contexts
{
    public abstract class VirtualCompanionExecutionContextFactoryTest<TVirtualCompanionExecutionContextFactory>
        where TVirtualCompanionExecutionContextFactory : IVirtualCompanionExecutionContextFactory
    {
        protected IVirtualCompanionExecutionContextFactory Instance { get; private set; }

        [TestInitialize]
        public void InitializeInstanceTestInitialize()
        {
            Instance = InitializeInstance();
        }

        protected virtual IVirtualCompanionExecutionContextFactory InitializeInstance()
        {
            return (IVirtualCompanionExecutionContextFactory)Activator.CreateInstance(typeof(TVirtualCompanionExecutionContextFactory));
        }

        [TestCleanup]
        public void CleanupInstanceTestCleanup()
        {
            CleanupInstance(Instance);
            Instance = null;
        }

        protected virtual void CleanupInstance(IVirtualCompanionExecutionContextFactory instance)
        {
            if(instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [TestMethod]
        public virtual async Task CreateVirtualCompanionExecutionContextAsync_ShouldReturnInstanceOfType()
        {
            var output = await Instance.CreateVirtualCompanionExecutionContextAsync();

            var expectedType = typeof(IVirtualCompanionExecutionContext);
            Assert.IsInstanceOfType(output, expectedType);
        }
    }
}
