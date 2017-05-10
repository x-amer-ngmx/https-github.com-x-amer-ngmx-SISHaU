using SISHaU.Library.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Library
{
    public class LibraryRepositoryFactory
    {
        public static Func<IUploadeResultRepository> FileBuilder = CreateDefultFileBuilder;

        private static IUploadeResultRepository CreateDefultFileBuilder()
        {
            throw new Exception("");
        }

        public IUploadeResultRepository FileRepositiryBuilder()
        {
            IUploadeResultRepository builder = FileRepositiryBuilder();
            return builder;
        }
    }
}
