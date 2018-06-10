using System;

namespace BetSquad.Models
{
    public class ErrorViewModel : ModelResult
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}