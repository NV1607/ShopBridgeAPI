using AutoMapper;
using ShopBridge.DataTransferObjects;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Utility
{
    /// <summary>
    /// ShopBridge AutoMapper use to 
    /// automatically map one object values to others
    /// </summary>
    public class ShopBridgeAutoMapper : Profile
    {
        /// <summary>
        /// Product Ship AutoMapper Constuctor
        /// </summary>
        public ShopBridgeAutoMapper()
        {
            ConfigureShopBridgeAutoMapper();
        }

        private void ConfigureShopBridgeAutoMapper()
        {
            CreateMap<Product, ProductResponseDataTransferObject>();
            CreateMap<AddProductRequestDataTransferObject, Product>();
            CreateMap<ModifyProductRequestDataTransferObject, Product>();

        }
    }
}
