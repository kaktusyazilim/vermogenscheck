using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Wrappers.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
