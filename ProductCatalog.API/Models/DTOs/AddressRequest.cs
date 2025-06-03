﻿namespace ProductCatalog.API.Models.DTOs
{
    public class AddressRequest
    {
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
