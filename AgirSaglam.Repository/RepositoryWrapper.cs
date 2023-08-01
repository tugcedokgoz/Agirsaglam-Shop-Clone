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


        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
