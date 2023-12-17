namespace Elearning.Contracts.Common
{
    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        #region Search Parameters
        public DateTime? Date { get; set; }
        public string? CaseType { get; set; }
        public string? PatientName { get; set; }
        public string? MedicalCaseCode { get; set; }
        public string? IdentityNumner { get; set; }
        #endregion
        public bool IsValid()
        {
            if (PageNumber < 1)
            {
                return false;
            }

            return true;
        }
    }
}
