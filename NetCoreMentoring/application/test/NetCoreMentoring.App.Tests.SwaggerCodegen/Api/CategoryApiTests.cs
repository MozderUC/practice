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

namespace IO.Swagger.Test
{
    /// <summary>
    ///  Class for testing CategoryApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class CategoryApiTests
    {
        private CategoryApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new CategoryApi("https://localhost:44380");
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of CategoryApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' CategoryApi
            //Assert.IsInstanceOfType(typeof(CategoryApi), instance, "instance is a CategoryApi");
        }

        /// <summary>
        /// Test GetCategories
        /// </summary>
        [Test]
        public void GetCategoriesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            string version = "1.0";
            instance.GetCategories(version);
            
        }
        /// <summary>
        /// Test GetPicture
        /// </summary>
        [Test]
        public void GetPictureTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? id = null;
            //string version = null;
            //instance.GetPicture(id, version);
            
        }
        /// <summary>
        /// Test UpdatePicture
        /// </summary>
        [Test]
        public void UpdatePictureTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string version = null;
            //string id = null;
            //instance.UpdatePicture(version, id);
            
        }
    }

}