using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderApp.Service;

public class PasswordValidationTests
{
    private readonly AuthService _service = new AuthService(null);

    [Theory]
    [InlineData("Password1", true)]
    [InlineData("pass", false)]
    [InlineData("password", false)]
    [InlineData("PASSWORD", false)]
    [InlineData("Pass123", false)]
    [InlineData("pass1234", false)]
    public void IsValidPassword_ChecksVariousInputs(string input, bool expected)
    {
        var result = _service.IsValidPassword(input);
        Assert.Equal(expected, result);
    }
}

