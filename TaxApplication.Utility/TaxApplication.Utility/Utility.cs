using System;

namespace TaxApplication.Utility
{
    public class ResourceString
    {
        public static string InvalidName { get { return "Please enter a valid name!"; } }
        public static string InvalidDate { get { return "Please enter a valid date format (yyyy-mm-dd)!"; } }
        public static string ErrorMessage { get { return "Oops, there was some error!"; } }
        public static string InvalidMunicipalityName { get { return "Please enter a valid municipality name!"; } }
        public static string InvalidTaxType { get { return "Please enter a valid tax type!"; } }
        public static string InvalidTaxSlabID { get { return "Please enter a valid Tax Slab Id !"; } }
        public static string InvalidTaxTypeandDates { get { return "Please enter a valid date range WRT tax type!"; } }
        public static string InvalidDates { get { return "TO date cannot be lesser than FROM Date!"; } }
        public static string NotFound { get { return "No data found!"; } }
    }
    
    public class ResultModel
    {
        public bool Status { get; set; }
        public object Result { get; set; }
    }
}
