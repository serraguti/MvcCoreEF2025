using Microsoft.EntityFrameworkCore;
using MvcCoreEF.Data;
using MvcCoreEF.Models;

namespace MvcCoreEF.Repositories
{
    public class RepositoryHospital
    {
        private HospitalContext context;
        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            var consulta = from datos in this.context.Hospitales
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Hospital> FindHospitalAsync(int idHospital)
        {
            var consulta = from datos in this.context.Hospitales
                           where datos.IdHospital == idHospital
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task 
            InsertHospitalAsync(int idHospital, string nombre
            , string direccion, string telefono, int camas)
        {
            //CREAMOS UN MODEL
            Hospital hospital = new Hospital();
            //ASIGNAMOS SUS PROPIEDADES
            hospital.IdHospital = idHospital;
            hospital.Nombre = nombre;
            hospital.Direccion = direccion;
            hospital.Telefono = telefono;
            hospital.Camas = camas;
            //AÑADIMOS NUESTRO MODEL A LA COLECCION DBSET DEL CONTEXT
            await this.context.Hospitales.AddAsync(hospital);
            //INDICAMOS QUE ALMACENE LOS DATOS EN LA BBDD
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteHospitalAsync(int idHospital)
        {
            //BUSCAMOS EL MODEL PARA ELIMINARLO
            Hospital hospital =
                await this.FindHospitalAsync(idHospital);
            //ELIMINAMOS DE LA COLECCION DbSet<T> DEL CONTEXT
            this.context.Hospitales.Remove(hospital);
            //ACTUALIZAMOS LA BASE DE DATOS
            await this.context.SaveChangesAsync();
        }

        public async Task 
            UpdateHospitalAsync(int idHospital, string nombre
            , string direccion, string telefono, int camas)
        {
            //BUSCAMOS EL OBJETO HOSPITAL A MODIFICAR
            Hospital hospital =
                await this.FindHospitalAsync(idHospital);
            //PODEMOS MODIFICAR TODO LO QUE DESEEMOS EXCEPTO
            //EL CAMPO [Key]
            hospital.Nombre = nombre;
            hospital.Direccion = direccion;
            hospital.Telefono = telefono;
            hospital.Camas = camas;
            //NO TENEMOS NINGUN METODO PARA REALIZAR UN UPDATE
            //DENTRO DEL CONTEXT Y DbSet<T>
            await this.context.SaveChangesAsync();
        }
    }
}
