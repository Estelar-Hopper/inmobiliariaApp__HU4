using inmobiliariaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using inmobiliariaApp.Domain.Entities;
using inmobiliariaApp.Domain.Interfaces;

namespace inmobiliariaApp.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {


        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context) // dependency injection
        {
            _context = context;
        }

        //---------------------------------------------------------------------------
        
        // get product by id
        public async Task<Property> GetPropertyById(int id)
        {
            return await _context.Properties.FirstOrDefaultAsync(p => p.Id == id);
        }
        

        // get all products
        public async Task<IEnumerable<Property>> GetAllProperty()
        {
            return await _context.Properties.ToListAsync();
        }
        
        
        // add new product
        public async Task<Property> AddProperty(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
            return property;
        }
        
        
        // update product
        public async Task<Property> UpdateProperty(Property property)
        {
            var existingProperty = await _context.Properties.FindAsync(property.Id);

            if (existingProperty == null)
                return null!;

            _context.Entry(existingProperty).CurrentValues.SetValues(property);
            await _context.SaveChangesAsync();
            return existingProperty;
        }
        
        
        // delete product
        public async Task DeleteProperty(int id)
        {
            var product = await _context.Properties.FindAsync(id);

            if (product != null)
            {
                _context.Properties.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
