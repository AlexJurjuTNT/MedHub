namespace MedHub_Backend.Service.Doctor;

public interface IDoctorService
{
    Task<List<Model.User>> GetAllDoctorsAsync();
    Task<Model.User?> GetDoctorById(int id);
    Task<bool> DeleteDoctorAsync(int id);
    Task<Model.User> UpdateDoctorAsync(Model.User doctor);
}