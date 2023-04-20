using System.Text;
using Sat.Recruitment.Api.Intefases;

namespace Sat.Recruitment.Api.Utils
{
    public class MethodResult
    {
        private StringBuilder _errors;

        public MethodResult()
        {
            _errors = new StringBuilder();
        }

        public bool IsSuccess
        {
            get => _errors.Length==0;
        }
        public StringBuilder Errors
        { 
            get =>_errors; 
            set => _errors = value; 
        }
    }
}
