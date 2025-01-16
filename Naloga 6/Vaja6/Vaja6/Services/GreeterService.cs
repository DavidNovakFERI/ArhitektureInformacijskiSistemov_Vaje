using Grpc.Core;
using Vaja6;
using Vaja6.Data;

namespace Vaja6.Services
{
    public class VoznikServices : Voznik.VoznikBase
    {
        AppDbContext _dbContext = new AppDbContext();
       

        public override Task<CreateVoznikResponse> CreateVoznik(CreateVoznikRequest request, ServerCallContext context)
        {
            Vozniki voznik1 = new Vozniki { Ime = request.Ime, Priimek = request.Priimek, Starost = request.Starost, ŠtevilkaFormule = request.Stevilkaformule, ŠteviloZmag = request.Stevilozmag};
            _dbContext.Vozniki.Add(voznik1);
            _dbContext.SaveChanges();

            var response = new CreateVoznikResponse
            {
                Id = voznik1.Id
            };

            return Task.FromResult(response);
        }

        public override Task<ReadVoznikResponse> ReadVoznik(ReadVoznikRequest request, ServerCallContext context)
        {
            

            var voznik1 = _dbContext.Vozniki.Find(request.Id);

            var response = new ReadVoznikResponse
            {
               
                Ime = voznik1.Ime,
                Priimek = voznik1.Priimek,
                Starost = voznik1.Starost,
                Stevilkaformule = voznik1.ŠtevilkaFormule,
                Stevilozmag = voznik1.ŠteviloZmag
            };

            return Task.FromResult(response);
        }

        public override Task<UpdateVoznikResponse> UpdateVoznik(UpdateVoznikRequest request, ServerCallContext context)
        {
            var voznik1 = _dbContext.Vozniki.Find(request.Id);
            voznik1.Ime = request.Ime;
            voznik1.Priimek = request.Priimek;
            voznik1.Starost = request.Starost;
            voznik1.ŠtevilkaFormule = request.Stevilkaformule;
            voznik1.ŠteviloZmag = request.Stevilozmag;
            _dbContext.SaveChanges();

            var response = new UpdateVoznikResponse
            {
                Id = voznik1.Id
            };
            return Task.FromResult(response);

        }

        public override Task<DeleteVoznikResponse> DeleteVoznik(DeleteVoznikRequest request, ServerCallContext context)
        {
            var voznik1 = _dbContext.Vozniki.Find(request.Id);
            _dbContext.Vozniki.Remove(voznik1);
            _dbContext.SaveChanges();

            var response = new DeleteVoznikResponse
            {
                Id = voznik1.Id
            };

            return Task.FromResult(response);

        }
    }

}
