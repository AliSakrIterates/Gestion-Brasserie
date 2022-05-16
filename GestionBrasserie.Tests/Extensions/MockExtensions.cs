using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBrasserie.Test
{
    public static class MockExtensions
    {
        public static Mock<T> AsMock<T>(this T instance) where T : class
        {
            return Mock.Get(instance);
        }
    }
}
