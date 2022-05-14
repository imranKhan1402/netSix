using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Custom
{
public static class CAMessage
    {
        public const string SUCCESS_MESSAGES = "SUCCEEDED";
        public const string ERROR_MESSAGES = "FAILED";
        public const string NO_ROWS_FOUND = "NO_ROWS_FOUND";



        public static string GetError = "Error";
        public static string PostSuccess = "Success";
        public static string ApiLinkError = "Link is Broken";
        public static string OraAPIController = "";
        public static string SqlAPIController = "";
        public static string NoRowsFound = "No Rows Found";


    }
}
