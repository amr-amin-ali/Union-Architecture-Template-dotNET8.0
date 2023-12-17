namespace Elearning.Entittes
{
    using Elearning.Entittes.DbContexts;

    public static class DataSeeder
    {
        public static async Task SeedData(ElearningContext context)
        {
            try
            {
                using (var _context = new ElearningContext())
                {
                    //await SeedPriorities(_context);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logMsg = ex.Message;
                throw;
            }
        }


        // Add more SeedTableX methods for each table
    }
}
