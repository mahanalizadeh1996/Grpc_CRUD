using Grpc.Core;
using TestRira.Entities;
using TestRira.Protos;
using TestRira.Repo;

namespace TestRira.Services
{
    public class CustomerGrpcService : CustomerService.CustomerServiceBase
    {
        private readonly IRepository<CustomerEntity> _customerRepository;
        public CustomerGrpcService(IRepository<CustomerEntity> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public override async Task<Empty> Post(Customer request, ServerCallContext context)
        {
            try
            {
                var customer = new CustomerEntity()
                {
                    Name = request.Name,
                    Family = request.Family,
                    ID = request.Id,
                    NationalCode = request.NationalCode
                };
                await _customerRepository.InsertAsync(customer);
                var result = await Task.FromResult(new Empty() { Status = 200, Message = "Success" });
                return result;
            }
            catch (Exception ex)
            {
                var result = await Task.FromResult(new Empty() { Status = -200, Message = "Failed" });
                return result;
            }
        }
        public override async Task<Customer> GetById(IdFilter request, ServerCallContext context)
        {
            var customer = await _customerRepository.GetByIdAsync(request.RowId);

            var customerResult = new Customer
            {
                Name = customer.Name,
                Family = customer.Family,
                NationalCode = customer.NationalCode,
                Id = customer.ID
            };

            return await Task.FromResult(customerResult);
        }

        public override async Task<Customers> GetAll(Empty request, ServerCallContext context)
        {
            var res=await _customerRepository.GetAllAsync();

            var customers = new Customers();

            foreach (var customeritem in res) 
            {
                var customer = new Customer
                {
                    Name = customeritem.Name,
                    Family = customeritem.Family,
                    NationalCode = customeritem.NationalCode,
                    Id = customeritem.ID
                };
                customers.Items.Add(customer);
            }

            return await Task.FromResult(customers);
        }

        public override async Task<Empty> Put(Customer request, ServerCallContext context)
        {
            try
            {
                var customer = new CustomerEntity()
                {
                    Name = request.Name,
                    Family = request.Family,
                    ID = request.Id,
                    NationalCode = request.NationalCode
                };
                await _customerRepository.UpdateAsync(customer);
                var result = await Task.FromResult(new Empty() { Status = 200, Message = "Success" });
                return result;
            }
            catch (Exception ex)
            {
                var result = await Task.FromResult(new Empty() { Status = 0, Message = "Failed" });
                return result;
            }
        }

        public override async Task<Empty> Delete(Customer request, ServerCallContext context)
        {
            try
            {
                var customer = new CustomerEntity()
                {
                    Name = request.Name,
                    Family = request.Family,
                    ID = request.Id,
                    NationalCode = request.NationalCode
                };
                await _customerRepository.DeleteAsync(customer);

                var result = await Task.FromResult(new Empty() { Status = 200, Message = "Success" });
                return result;
            }
            catch (Exception ex)
            {
                var result = await Task.FromResult(new Empty() { Status = -200, Message = "Failed" });
                return result;
            }
        }
    }
}
