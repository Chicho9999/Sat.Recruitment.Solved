namespace Sat.Recruitment.Api.Helper
{
    public static class ErrorValidationHelper
    {
        /// <summary>
        /// Validate if the form comes with empty information
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <param name="errors"></param>
        public static string ValidateErrors(string name, string email, string address, string phone)
        {
            var errors = "";
            if (name == null)
                //Validate if Name is null
                errors = "The name is required";
            if (email == null)
                //Validate if Email is null
                errors = errors + " The email is required";
            else
            {
                if (!email.Contains('@') || !email.ToUpper().Contains(".COM"))
                    //Validate if Email is properly written
                    errors = errors + " The email is invalid";
            }
            if (address == null)
                //Validate if Address is null
                errors = errors + " The address is required";
            
            if (phone == null)
                //Validate if Phone is null
                errors = errors + " The phone is required";

            return errors;
        }
    }
}