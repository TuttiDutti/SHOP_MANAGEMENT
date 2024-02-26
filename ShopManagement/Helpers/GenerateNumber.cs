namespace ShopManagement.Helpers
{
    public class GenerateNumber
    {
        public static string GenerateNextNumber(string lastOrder, string type, string login)
        {
            int nextNumber = 1;
            if(lastOrder != null)
            {
                nextNumber = Int32.Parse(lastOrder.Split('/').Last()) +1;
            }

            return $"{type}/{login}/{nextNumber:D4}";
        }
    }
}
