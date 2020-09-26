using System;

namespace Lab_2
{
    public class ProductException : Exception
    {
        public ProductException()
        {
        }

        public ProductException(string message)
            : base(message)
        {
        }

        public ProductException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ProductPriceNotSetException : ProductException
    {
        public ProductPriceNotSetException()
        {
        }

        public ProductPriceNotSetException(string message)
            : base(message)
        {
        }

        public ProductPriceNotSetException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ProductAmountNotSetException : ProductException
    {
        public ProductAmountNotSetException()
        {
        }

        public ProductAmountNotSetException(string message)
            : base(message)
        {
        }

        public ProductAmountNotSetException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class ProductAmountNotEnoughException : ProductException
    {
        public ProductAmountNotEnoughException()
        {
        }

        public ProductAmountNotEnoughException(string message)
            : base(message)
        {
        }

        public ProductAmountNotEnoughException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    
    public class StoreNotFoundException : Exception
    {
        public StoreNotFoundException()
        {
        }

        public StoreNotFoundException(string message)
            : base(message)
        {
        }

        public StoreNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}