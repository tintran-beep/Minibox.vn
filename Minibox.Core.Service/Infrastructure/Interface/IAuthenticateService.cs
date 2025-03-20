using Minibox.Shared.Model.ViewModel;
using Minibox.Shared.Model.ViewModel.BaseVM;

namespace Minibox.Core.Service.Infrastructure.Interface
{
	public interface IAuthenticateService
	{
		/// <summary>
		/// Registers a new user with the provided sign-up details.
		/// </summary>
		/// <param name="model">The sign-up model containing user details.</param>
		/// <returns>A response containing the signed-in user details after successful registration.</returns>
		Task<ResponseVM<UserSignedInVM>> SignUpAsync(UserSignUpVM model);

		/// <summary>
		/// Authenticates a user with the provided sign-in credentials.
		/// </summary>
		/// <param name="model">The sign-in model containing username and password.</param>
		/// <returns>A response containing the signed-in user details if authentication is successful.</returns>
		Task<ResponseVM<UserSignedInVM>> SignInAsync(UserSignInVM model);

		/// <summary>
		/// Authenticates an administrator with the provided sign-in credentials.
		/// </summary>
		/// <param name="model">The sign-in model containing admin username and password.</param>
		/// <returns>A response containing the signed-in admin user details if authentication is successful.</returns>
		Task<ResponseVM<UserSignedInVM>> AdminSignInAsync(UserSignInVM model);

		/// <summary>
		/// Generates an OTP secret key for a given user and returns a QR code for authentication apps.
		/// </summary>
		/// <param name="model">The user information required to generate the OTP secret.</param>
		/// <returns>
		/// A response containing the user's OTP secret key and a QR code in base64 format,  
		/// or an error message if the process fails.
		/// </returns>
		Task<ResponseVM<UserGeneratedOtpVM>> GenerateOtpAsync(UserGenerateOtpVM model);

		/// <summary>
		/// Verifies the OTP code entered by the user and generates an access token if successful.
		/// </summary>
		/// <param name="model">The request containing the username and OTP code for verification.</param>
		/// <returns>
		/// Returns a success response with an access token if the OTP is valid.  
		/// Returns a failure response with an error message if verification fails.
		/// </returns>
		Task<ResponseVM<UserVerifiedOtpVM>> VerifyOtpAsync(UserVerifyOtpVM model);

		/// <summary>
		/// Signs out the user with the specified user ID.
		/// </summary>
		/// <param name="userId">The unique identifier of the user to sign out.</param>
		Task SignOutAsync(Guid userId);
	}
}
