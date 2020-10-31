﻿using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCatagory;
using ShopManagement.Domain.ProductCatagoryAgg;
using System;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductCatagoryApplication : IProductCatagoryApplication
    {
        private readonly IProductCatagoryRepository repo;

        public ProductCatagoryApplication(IProductCatagoryRepository repo)
        {
            this.repo = repo;
        }
        public OperationResult Create(CreateProductCatagory command)
        {
            var operation = new OperationResult();
            if (repo.Exist(c => c.Name == command.Name))
                return operation.Failed("این نام تکراری است. لطفا نام دیگری انتخاب کنید");
            var productCatagory = new ProductCatagory(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, command.Slug);
            repo.Create(productCatagory);
            repo.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductCatagory command)
        {
            var operation = new OperationResult();
            var productCatagory = repo.Get(command.Id);
            if (productCatagory == null)
                return operation.Failed("رکوردی یافت نشد");

            if (repo.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("این نام تکراری است. لطفا نام دیگری انتخاب کنید");

            productCatagory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, command.Slug);
            repo.SaveChanges();
            return operation.Succedded("ویرایش اطلاعات با موفقیت انجام شد");
        }

        public ProductCatagory GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductCatagoryViewModel> Search(ProductCatagorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}