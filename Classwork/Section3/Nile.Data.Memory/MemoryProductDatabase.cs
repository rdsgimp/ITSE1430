﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.Memory
{
    /// <summary>Provides an in-memory product database.</summary>
    public class MemoryProductDatabase
    {
        public MemoryProductDatabase()
        {
            //seed products
           // _products = new Product[25];

            var product = new Product();
            product.Id = _nextId++;
            product.Name = "iFonY";
            product.IsDiscontinued = true;
            product.Price = 1500;
            _products.Add(product);

            product = new Product();
            product.Id = _nextId++;

            product.Name = "Explody Samsung";
            product.IsDiscontinued = true;
            product.Price = 1141;
            _products.Add(product);

            product = new Product();
            product.Id = _nextId++;

            product.Name = "ElGee G7";
            product.IsDiscontinued = false;
            product.Price = 755;
            _products.Add(product);

            // _products[3].
        }
        public Product Add (Product product, out string message)
        {
            // check for null
            if (product == null)
            {
                message = "Product cannot be null";
                return null;
            }

            //validate product
            var error = product.Validate();
            if (!String.IsNullOrEmpty(error))
            {
                message = error;
                return null;
            }

            //TODO: Verify unique product

            //add
            //var index = FindEmptyProductIndex();
            //if (index < 0)
            //{
            //    message = "Out of memory";
            //    return null;
            //}

            //clone the object
            product.Id = _nextId++;
            _products.Add(Clone(product));
            message = null;

            //todo: return a copy
            return product;
        }

        public Product Edit( Product product, out string message )
        {
            // check for null
            if (product == null)
            {
                message = "Product cannot be null";
                return null;
            }

            //validate product
            var error = product.Validate();
            if (!String.IsNullOrEmpty(error))
            {
                message = error;
                return null;
            }

            //TODO: Verify unique product except current

            //find existing
            var existing = GetById(product.Id);            
            if (existing == null)
            {
                message = "Product not found";
                return null;
            }

            // clone object
            // _products[existingIndex] = Clone(product);
            Copy(existing, product);
            message = null;
            //return a copy
            return product;


        }

        public Product[] GetAll ()
        {
            var items = new List<Product>();
            //for (var index = 0; index < _products.Length; ++index)
            foreach (var product in _products)
            {
                if (product != null)
                items.Add(Clone(product));
            }

            return items.ToArray();
        }

        public void Remove (int id)
        {
            if (id > 0)
            {
                var existing = GetById(id);
                if (existing != null)
                    _products.Remove(existing);
            }
        }
        private Product GetById(int id)
        {
           // for(var index = 0; index < _products.Length; ++index)
           foreach (var product in _products)
            {
                if (product.Id == id)
                    return product;
            }
            return null;
        }

        private Product Clone (Product item)
        {
            var newProduct = new Product();
            Copy(newProduct, item);

            return newProduct;
        }

        private void Copy(Product target, Product source)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.IsDiscontinued = source.IsDiscontinued;
        }

        //private int FindEmptyProductIndex()
        //{
        //    for (var index = 0; index < _products.Length; ++index)
        //    {
        //        if (_products[index] == null)
        //            return index;
        //    }
        //    return -1;
        //}

        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;
    }
}