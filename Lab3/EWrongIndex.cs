using System;

namespace MyBaseList
{
    class EWrongIndex : Exception
    {
        public EWrongIndex(string message) : base(message)
        { }
    }
}