/* 
 * My API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using IO.Swagger.Client;
using IO.Swagger.Api;
using IO.Swagger.Model;

namespace IO.Swagger.Test
{
    /// <summary>
    ///  Class for testing ProductApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class ProductApiTests
    {
        private ProductApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new ProductApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of ProductApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' ProductApi
            //Assert.IsInstanceOfType(typeof(ProductApi), instance, "instance is a ProductApi");
        }

        /// <summary>
        /// Test CreateProduct
        /// </summary>
        [Test]
        public void CreateProductTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string version = null;
            //ProductViewModel body = null;
            //instance.CreateProduct(version, body);
            
        }
        /// <summary>
        /// Test EditProduct
        /// </summary>
        [Test]
        public void EditProductTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? id = null;
            //string version = null;
            //ProductViewModel body = null;
            //instance.EditProduct(id, version, body);
            
        }
        /// <summary>
        /// Test GetProducts
        /// </summary>
        [Test]
        public void GetProductsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string version = null;
            //instance.GetProducts(version);
            
        }
    }

}