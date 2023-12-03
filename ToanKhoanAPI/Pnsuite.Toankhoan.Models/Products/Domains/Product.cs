using System;
using System.Collections.Generic;
using System.Linq;

namespace vn.com.pnsuite.toankhoan.models.Products.Domains
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ShortName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public bool IsHasExtraInfo { get; set; }
        public bool AutoCopyContent { get; set; }
        public bool ShowDetailInInvoice { get; set; }
        public string ProductCategoryName { get; set; }
        public int PriceTypeId { get; set; }
        public string PriceType { get; set; }
        public List<ProductInventory> ProductInventories { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public List<ProductUnit> ProductUnits { get; set; }
        public List<ProductHasExtraInfo> ProductHasExtraInfos { get; set; }



        // Memory 
        public string ExtraInfoName
        {
            get
            {
                if (ProductHasExtraInfos != null && ProductHasExtraInfos.Count > 0)
                {
                    return string.Join(",", ProductHasExtraInfos.Select(item => item.InputExtraInfoName));
                }
                return string.Empty;
            }
        }

        public string ExchangeBaseUnitName
        {
            get
            {
                if (ProductUnits != null && ProductUnits.Count > 0)
                {
                    return string.Join(",", ProductUnits.Select(item => item.UnitName));
                }
                return string.Empty;
            }
        }

        public string BaseUnitName
        {
            get
            {
                if (ProductUnits != null && ProductUnits.Count > 0)
                {
                    var infoBaseUnit = ProductUnits.FirstOrDefault(item => item.IsBaseUnit);
                    if (infoBaseUnit != null)
                    {
                        return infoBaseUnit.UnitName;
                    }
                }
                return string.Empty;
            }
        }

        public float OnHandOnDefaulWareHouse
        {
            get
            {
                if (ProductInventories != null && ProductInventories.Count > 0)
                {
                    var infoInventory = ProductInventories.FirstOrDefault(item => item.IsDefaultWarehouse);
                    if (infoInventory != null)
                    {
                        return infoInventory.StockValue;
                    }
                }
                return default;
            }
        }
    }
}


