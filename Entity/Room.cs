//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        public string hotel_id { get; set; }
        public int room_No { get; set; }
        public string room_type { get; set; }
        public int price_adult { get; set; }
        public int price_child { get; set; }
    
        public virtual Hotel Hotel { get; set; }
    }
}