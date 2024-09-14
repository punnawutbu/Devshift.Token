using System;

namespace Devshift.Token.Models
{
    public class ElkProductRes
    {
        public int Id {get; set;}
        public string Token {get; set;}
        public string SystemName {get; set;}
        public DateTime? CreatedAt {get; set;}
    }
}