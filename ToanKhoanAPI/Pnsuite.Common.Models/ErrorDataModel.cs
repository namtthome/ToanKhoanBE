using Microsoft.Data.SqlClient;
using System;

namespace vn.com.pnsuite.common.models
{
    public class ErrorDataModel
    {
        public ErrorDataModel() { }
        public ErrorDataModel(Exception exception)
        {
            if (exception is SqlException)
            {
                SqlException ex = (SqlException)exception;
                ErrorCode = ex.Number.ToString();
                ErrorMessage = ex.Message;
            } else
            {
                ErrorCode = exception.Source;
                ErrorMessage = exception.Message;
            }

        }

        public ErrorDataModel(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Object Data { get; set; }
    }
}
