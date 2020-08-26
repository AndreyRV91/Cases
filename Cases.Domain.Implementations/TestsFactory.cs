using Cases.Domain.Contracts;
using Cases.Domain.Implementations.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cases.Domain.Implementations
{
    static class TestsFactory
    {
        private static readonly List<ITest> _list = new List<ITest>();

        static TestsFactory()
        {
            _list.Add(new TestYouTubeLike());
            _list.Add(new TestYouTubeComment());
            _list.Add(new TestYouTubeSearch());
        }

        public static ITest GetTest(int id)
        {
            try
            {
                return _list[id];
            }
            catch
            {
                throw new Exception("Unsupported test type");
            }
        }
    }


}

