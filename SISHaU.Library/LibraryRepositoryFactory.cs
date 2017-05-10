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
        public static Func<IFileBuilder> FileBuilder = CreateDefultFileBuilder;

        private static IFileBuilder CreateDefultFileBuilder()
        {
            throw new Exception("");
        }

        public IFileBuilder FileRepositiryBuilder()
        {
            IFileBuilder builder = FileRepositiryBuilder();
            return builder;
        }
    }
}
