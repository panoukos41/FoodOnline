namespace FoodOnline.Domain
{
    public static class Policy
    {
        /// <summary>
        /// Only admin and foodonline employee
        /// </summary>
        public const string Staff = "FoodOnline_Policy";

        /// <summary>
        /// Only admin, foodonline employee and store owner
        /// </summary>
        public const string StaffAndOwner = "FoodOnline_Owner_Policy";

        /// <summary>
        /// Only admin, foodonline employee, store owner and store employee
        /// </summary>
        public const string StaffAndCustomer = "FoodOnline__Owner_Employees_Policy";

        /// <summary>
        /// Only store owner and store employee
        /// </summary>
        public const string Customer = "Owner_Employees_Policy";
    }
}