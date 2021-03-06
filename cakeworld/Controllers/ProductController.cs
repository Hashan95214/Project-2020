﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cakeworld.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace cakeworld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=cakecontainer;AccountKey=JDCsdPOW3hZjoMuz9ErrS0FrfUtVNCA9t+UEukQE+dWn44dStQ6wTsNisk37cfaI66CD5ELEmcA5VHH9kbuypw==;EndpointSuffix=core.windows.net");
        private readonly OnlineDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(OnlineDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }


        // GET: api/Product
        [HttpGet]
        [Route("Accept")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            return await _context.Products
                 .Select(x => new ProductModel()
                 {
                     ProductID = x.ProductID,
                     ProductName = x.ProductName,
                     Price = x.Price,
                     Category = x.Category,
                     Description = x.Description,
                     ImageName = x.ImageName,
                     SellerID = x.SellerID,
                     Conformation = x.Conformation,

                     ImageSrc = String.Format(" https://cakecontainer.blob.core.windows.net/image/{0}", x.ImageName)
                 })

                .ToListAsync();
        }










        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts(string Conformation)
        {
            return await _context.Products
                 .Select(x => new ProductModel()
                 {
                     ProductID = x.ProductID,
                     ProductName = x.ProductName,
                     Price = x.Price,
                     Category = x.Category,
                     Description = x.Description,
                     ImageName = x.ImageName,
                     SellerID = x.SellerID,
                     Conformation = x.Conformation,
                     ImageSrc = String.Format("https://cakecontainer.blob.core.windows.net/image/{0}", x.ImageName)
                 })
                  .Where(x => x.Conformation == "Ok")
                .ToListAsync();
        }


        [HttpGet("{para}/{para2}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts(string para, int para2)
        {
            return await _context.Products
                 .Select(x => new ProductModel()
                 {
                     ProductID = x.ProductID,
                     ProductName = x.ProductName,
                     Price = x.Price,
                     Category = x.Category,
                     Description = x.Description,
                     ImageName = x.ImageName,
                     ImageSrc = String.Format("https://cakecontainer.blob.core.windows.net/image/{0}", x.ImageName),
                     SellerID = x.SellerID
                 })
                 .Where(i => i.SellerID == para2)
                .ToListAsync();
        }


        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
            var ProductModel = await _context.Products
                .Select(x => new ProductModel()
                {
                    ProductID = x.ProductID,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Category = x.Category,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("https://cakecontainer.blob.core.windows.net/image/{0}", x.ImageName),
                    SellerID = x.SellerID
                })
                 .Where(i => i.ProductID == id)
                .FirstOrDefaultAsync();


            if (ProductModel == null)
            {
                return NotFound();
            }

            return ProductModel;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, [FromForm] ProductModel productModel)
        {
            if (id != productModel.ProductID)
            {
                return BadRequest();
            }

            if (productModel.ImageFile != null)
            {
                DeleteImage(productModel.ImageName);
                productModel.ImageName = await SaveImage(productModel.ImageFile);
            }

            _context.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductModel([FromForm] ProductModel productModel)
        {
            productModel.ImageName = await SaveImage(productModel.ImageFile);
            _context.Products.Add(productModel);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductModel>> DeleteProductModel(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            DeleteImage(productModel.ImageName);
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).ToArray());
            imageName = imageName + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            //using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await UploadToAzureAsync(imageFile);
            }
            return imageName;

        }

        private async Task UploadToAzureAsync(IFormFile imageFile)
        {
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var cloudBlobContainer = cloudBlobClient.GetContainerReference("image");

            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            }

            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageFile.FileName);
            cloudBlockBlob.Properties.ContentType = imageFile.ContentType;

            await cloudBlockBlob.UploadFromStreamAsync(imageFile.OpenReadStream());
        }





        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }


}

