using Dapper;
using KykKaliteApi.Dtos.HMPatamaDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HMPatamaRepository
{
    public class HMPatamaRepository : IHMPatamaRepository
    {
        private readonly Context _context;
        public HMPatamaRepository(Context context)
        {
            _context = context;
        }

        public async void CreateHMPatama(CreateHMPatamaDto createHMPatamaDto)
        {
            string query = "insert into HMPatamaAktif (ParametreKodu,HammaddeId,ParametreId,HMPAtamaKodu,ParametreKritiklikSeviyesi,KontrolDegeriNominal,AltOnaySiniri,UstOnaySiniri,AltSartliKabulSiniri,UstSartliKabulSiniri,CihazId,ReferansDokuman,Aciklama,FabrikaId,PersonelSicilNo,OlusturmaTarihi,KullanimDurumu,MevcutPartiBuyuklugu,TedarikSikligi,TedarikSikligiOrtalama,TedarikSikligiBirim,Versiyon,ParametreYonu) values (@parametreKodu,@hammaddeId,@parametreId,@hMPAtamaKodu,@parametreKritiklikSeviyesi,@kontrolDegeriNominal,@altOnaySiniri,@ustOnaySiniri,@altSartliKabulSiniri,@ustSartliKabulSiniri,@cihazId,@referansDokuman,@aciklama,@fabrikaId,@personelSicilNo,@olusturmaTarihi,@kullanimDurumu,@mevcutPartiBuyuklugu,@tedarikSikligi,@tedarikSikligiOrtalama,@tedarikSikligiBirim,@versiyon, @parametreYonu)";
            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", createHMPatamaDto.ParametreKodu);
            parameters.Add("@parametreId", createHMPatamaDto.ParametreId);
            parameters.Add("@hammaddeId", createHMPatamaDto.HammaddeId);
            parameters.Add("@hMPAtamaKodu", createHMPatamaDto.HMPAtamaKodu);
            parameters.Add("@parametreKritiklikSeviyesi", createHMPatamaDto.ParametreKritiklikSeviyesi);
            parameters.Add("@kontrolDegeriNominal", createHMPatamaDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", createHMPatamaDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", createHMPatamaDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", createHMPatamaDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", createHMPatamaDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", createHMPatamaDto.CihazId);
            parameters.Add("@referansDokuman", createHMPatamaDto.ReferansDokuman);
            parameters.Add("@fabrikaId", createHMPatamaDto.FabrikaId);
            parameters.Add("@personelSicilNo", createHMPatamaDto.PersonelSicilNo);
            parameters.Add("@olusturmaTarihi", createHMPatamaDto.OlusturmaTarihi);
            parameters.Add("@aciklama", createHMPatamaDto.Aciklama);
            parameters.Add("@mevcutPartiBuyuklugu", createHMPatamaDto.MevcutPartiBuyuklugu);
            parameters.Add("@tedarikSikligi", createHMPatamaDto.TedarikSikligi);
            parameters.Add("@tedarikSikligiOrtalama", createHMPatamaDto.TedarikSikligiOrtalama);
            parameters.Add("@tedarikSikligiBirim", createHMPatamaDto.TedarikSikligiBirim);
            parameters.Add("@versiyon", createHMPatamaDto.Versiyon);
            parameters.Add("@parametreYonu", createHMPatamaDto.ParametreYonu);
            parameters.Add("@kullanimDurumu", true); ;
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultHMPatamaDto>> GetAllHMPatamaAsync()
        {
            string query = "Select h.MalzemeAciklamasi, * From HMPatamaAktif inner join Hammaddeler h on h.HammaddeID = HMPatamaAktif.HammaddeID";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultHMPatamaDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateHMPatama(UpdateHMPatamaDto updateHMPatamaDto)
        {
            string query = @"
        UPDATE HMPatamaAktif 
        SET 
            ParametreKodu = @parametreKodu,
            HammaddeId = @hammaddeId,
            ParametreId = @parametreId,
            HMPAtamaKodu = @hMPAtamaKodu,
            ParametreKritiklikSeviyesi = @parametreKritiklikSeviyesi,
            KontrolDegeriNominal = @kontrolDegeriNominal,
            AltOnaySiniri = @altOnaySiniri,
            UstOnaySiniri = @ustOnaySiniri,
            AltSartliKabulSiniri = @altSartliKabulSiniri,
            UstSartliKabulSiniri = @ustSartliKabulSiniri,
            CihazId = @cihazId,
            ReferansDokuman = @referansDokuman,
            Aciklama = @aciklama,
            FabrikaId = @fabrikaId,
            PersonelSicilNo = @personelSicilNo,
            OlusturmaTarihi = @olusturmaTarihi,
            KullanimDurumu = @kullanimDurumu,
            MevcutPartiBuyuklugu = @mevcutPartiBuyuklugu,
            TedarikSikligi = @tedarikSikligi,
            TedarikSikligiOrtalama = @tedarikSikligiOrtalama,
            TedarikSikligiBirim = @tedarikSikligiBirim,
            Versiyon = @versiyon
        WHERE HMPAtamaKodu = @hMPAtamaKodu";  // Assuming HMPAtamaKodu is unique or a key for the update

            var parameters = new DynamicParameters();
            parameters.Add("@parametreKodu", updateHMPatamaDto.ParametreKodu);
            parameters.Add("@parametreId", updateHMPatamaDto.ParametreID);
            parameters.Add("@hammaddeId", updateHMPatamaDto.HammaddeId);
            parameters.Add("@hMPAtamaKodu", updateHMPatamaDto.HmpatamaKodu);
            parameters.Add("@parametreKritiklikSeviyesi", updateHMPatamaDto.ParametreKritiklikSeviyesi);
            parameters.Add("@kontrolDegeriNominal", updateHMPatamaDto.KontrolDegeriNominal);
            parameters.Add("@altOnaySiniri", updateHMPatamaDto.AltOnaySiniri);
            parameters.Add("@ustOnaySiniri", updateHMPatamaDto.UstOnaySiniri);
            parameters.Add("@altSartliKabulSiniri", updateHMPatamaDto.AltSartliKabulSiniri);
            parameters.Add("@ustSartliKabulSiniri", updateHMPatamaDto.UstSartliKabulSiniri);
            parameters.Add("@cihazId", updateHMPatamaDto.CihazId);
            parameters.Add("@referansDokuman", updateHMPatamaDto.ReferansDokuman);
            parameters.Add("@fabrikaId", updateHMPatamaDto.FabrikaId);
            parameters.Add("@personelSicilNo", updateHMPatamaDto.PersonelSicilNo);
            parameters.Add("@olusturmaTarihi", updateHMPatamaDto.OlusturmaTarihi);
            parameters.Add("@aciklama", updateHMPatamaDto.Aciklama);
            parameters.Add("@mevcutPartiBuyuklugu", updateHMPatamaDto.MevcutPartiBuyuklugu);
            parameters.Add("@tedarikSikligi", updateHMPatamaDto.TedarikSikligi);
            parameters.Add("@tedarikSikligiOrtalama", updateHMPatamaDto.TedarikSikligiOrtalama);
            parameters.Add("@tedarikSikligiBirim", updateHMPatamaDto.TedarikSikligiBirim);
            parameters.Add("@versiyon", updateHMPatamaDto.Versiyon);
            parameters.Add("@kullanimDurumu", updateHMPatamaDto.KullanimDurumu); // Assuming KullanimDurumu can also be updated

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

    }
}
