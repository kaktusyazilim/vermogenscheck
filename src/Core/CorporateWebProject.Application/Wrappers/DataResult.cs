using CorporateWebProject.Application.Wrappers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Wrappers
{
    public class DataResult<T> : IDataResult<T> where T : class
    {
        public T Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Success { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Error { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
