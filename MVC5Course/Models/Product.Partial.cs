namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱！")]
        [MinLength(3), MaxLength(30)]
        [RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤！")]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [DisplayName("商品名稱")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "請輸入商品價格！")]
        [Range(0, 9999, ErrorMessage = "請設定正確的商品價格範圍！")]
        [DisplayFormat(DataFormatString ="{0:0}",ApplyFormatInEditMode = true)]
        [DisplayName("商品價格")]
        public Nullable<decimal> Price { get; set; }

        [Required(ErrorMessage = "請輸入商品狀態！")]
        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }

        [Required(ErrorMessage = "請輸入商品數量！")]
        [Range(0, 100, ErrorMessage = "請設定正確的商品數量範圍！")]
        [DisplayName("商品庫存")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
