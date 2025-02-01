namespace yogago.Models
{
    public class invoiceinfonew
    {
        public decimal InvoiceId { get; set; }
        public decimal? MemberPlanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? GeneratedDate { get; set; }
        public bool EmailSent { get; set; }

        // Related Memberplan details
        public string? PlanName { get; set; }
        public decimal? PlanPrice { get; set; }
        public DateTime? PlanStartDate { get; set; }

        // Related Member details
        public string? MemberName { get; set; }
    }
}
