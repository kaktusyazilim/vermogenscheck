﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException()
            : base()
        { }

        public RepositoryException(String message)
            : base(message)

        { }

        public RepositoryException(String message, Exception innerException)
            : base(message, innerException)
        { }



        protected RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}
