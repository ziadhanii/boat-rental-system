﻿using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoatSystem.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await GetByIdAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersByUserIdAsync(string userId)
        {
            return await _context.Customers.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await _context.Customers.AnyAsync(predicate);
        }

        public async Task<int?> GetCustomerIdByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                // يمكن أن تقرر ماذا تفعل إذا كان userId فارغاً أو null، مثل تسجيل رسالة خطأ أو إرجاع null.
                return null;
            }

            // افترض أن لديك DbContext أو أي مصدر بيانات آخر
            var customerId = await _context.Customers
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            return customerId; // هذا يمكن أن يكون null إذا لم يتم العثور على العميل
        }

        // أضف هذه الطريقة

        public async Task<Customer> GetOwnerByUserIdAsync(string userId)
        {
            var Customer = await _context.Customers
                .FirstOrDefaultAsync(o => o.UserId == userId);

            if (Customer == null)
            {
                Console.WriteLine($"Owner not found for UserId: {userId}");
            }
            else
            {
                Console.WriteLine($"Owner found: ID = {Customer.Id}");
            }

            return Customer;
        }

        public Task<IEnumerable<Customer>> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
