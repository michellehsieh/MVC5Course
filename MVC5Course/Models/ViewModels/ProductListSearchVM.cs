using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductListSearchVM: IValidatableObject

    {
        public string q { get; set; }
        public int from { get; set; }
        public int to { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.to < this.from) {
                yield return new ValidationResult("庫存資料篩選條件錯誤", new string[] { "from", "to" });
            }
        } 
    }
}