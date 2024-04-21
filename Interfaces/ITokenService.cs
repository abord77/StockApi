using LearningApi.Models;

namespace LearningApi.Interfaces {
    public interface ITokenService {
        string CreateToken(AppUser user);
    }
}
