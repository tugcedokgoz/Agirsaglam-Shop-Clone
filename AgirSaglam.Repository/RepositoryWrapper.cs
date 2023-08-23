using AgirSaglam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    public class RepositoryWrapper
    {
        private RepositoryContext context;

        private CategoryRepository categoryRepository;
        private ProductRepository productRepository;
        private RoleRepository roleRepository;
        private UserRepository userRepository;
        private AdressRepository adressRepository;
        private BillRepository billRepository;
        private CommentRepository commentRepository;
        private OrderRepository orderRepository;
        private ProductCategoryRepository productCategoryRepository;
        private PropertyGroupRepository propertyGroupRepository;
        private PropertyRepository propertyRepository;
        private ProductPropertyRepository productPropertyRepository;
        private CategoryPropertyRepository categoryPropertyRepository;
        private ContactRepository contactRepository;

        public RepositoryWrapper(RepositoryContext context)
        {
            this.context = context;
        }

        //kategori 
        public CategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(context);
                return categoryRepository;
            }
        }
        //ürün
        public ProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(context);
                return productRepository;

            }
        }
        //rol
        public RoleRepository RoleRepository
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(context);
                return roleRepository;

            }
        }
        //user
        public UserRepository UserRepository
        {
            get
            {
                if(userRepository==null)
                    userRepository=new UserRepository(context);
                return userRepository;
            }
        }
        //adress
        public AdressRepository AdressRepository
        {
            get
            {
                if (adressRepository == null)
                    adressRepository = new AdressRepository(context);
                return adressRepository;
            }
        }
        //bill
        public BillRepository BillRepository
        {
            get
            {
                if (billRepository == null)
                    billRepository = new BillRepository(context);
                return billRepository;
            }
        }
        //comment
        public CommentRepository CommentRepository
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new CommentRepository(context);
                return commentRepository;
            }
        }
        //order
        public OrderRepository OrderRepository
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(context);
                return orderRepository;
            }
        }
        //productcategory
        public ProductCategoryRepository ProductCategoryRepository
        {
            get
            {
                if (productCategoryRepository == null)
                    productCategoryRepository = new ProductCategoryRepository(context);
                return productCategoryRepository;
            }
        }
        //propertygroup
        public PropertyGroupRepository PropertyGroupRepository
        {
            get
            {
                if (propertyGroupRepository == null)
                    propertyGroupRepository = new PropertyGroupRepository(context);
                return propertyGroupRepository;
            }
        }
        //property
        public PropertyRepository PropertyRepository
        {
            get
            {
                if (propertyRepository == null)
                    propertyRepository = new PropertyRepository(context);
                return propertyRepository;
            }
        }
        //productproperty
        public ProductPropertyRepository ProductPropertyRepository
        {
            get
            {
                if (productPropertyRepository == null)
                    productPropertyRepository = new ProductPropertyRepository(context);
                return productPropertyRepository;
            }
        }
        //categoryproperty
        public CategoryPropertyRepository CategoryPropertyRepository
        {
            get
            {
                if (categoryPropertyRepository == null)
                    categoryPropertyRepository = new CategoryPropertyRepository(context);
                return categoryPropertyRepository;
            }
        }
        //contact
        public ContactRepository ContactRepository
        {
            get
            {
                if (contactRepository == null)
                    contactRepository = new ContactRepository(context);
                return contactRepository;
            }
        }
        public void SaveChanges()
        {
            context.SaveChanges();
         
        }
    }
}
